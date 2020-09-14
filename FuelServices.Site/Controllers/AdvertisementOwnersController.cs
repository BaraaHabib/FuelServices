using AutoMapper;
using DBContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class AdvertisementOwnersController : BaseController
    {
        private string userId;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdvertisementOwnersController(AirportCoreContext context, IServiceProvider serviceProvider) 
            :base(context,serviceProvider)
        {
        }

        // GET: AdvertisementOwners/Edit/5
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Where(r => r.Equals("AdOwner")).FirstOrDefault() != null)
            {
                return RedirectToAction("Create", "Advertisements");
            }
            ViewData["CountryId"] = new SelectList(db.Country.Where(a => !a.IsDeleted), "Id", "Name");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            if (user != null)
            {
                if (User.IsInRole("AdOwner"))
                {
                    AdvertisementOwner advertisementOwner = db.AdvertisementOwner.Where(a => a.UserId == userId).FirstOrDefault();
                    if (advertisementOwner != null)
                    {
                        return View(advertisementOwner);
                    }
                }
            }
            return View();
        }

        // POST: AdvertisementOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> PostCreateAsync(AdvertisementOwner advertisementOwner)
        {
            try
            {
                var mapper = GetService<IMapper>();


                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }

                advertisementOwner.UserId = userId;
                advertisementOwner.UserSpecializationId = 1;

                AdvertisementOwner local = db.AdvertisementOwner.Where(a => a.UserId == userId).FirstOrDefault();
                if (local != null)
                {
                    advertisementOwner.Id = local.Id;
                    local = mapper.Map<AdvertisementOwner>(advertisementOwner);
                    db.Update(local);
                    db.SaveChanges();
                }
                else
                {
                    local = mapper.Map<AdvertisementOwner>(advertisementOwner);
                    db.AdvertisementOwner.Add(local);
                    db.SaveChanges();

                }

                var user = _userManager.FindByIdAsync(userId).Result;
                if (user != null)
                {
                    if (!User.IsInRole("AdOwner"))
                    {
                        await _userManager.AddToRoleAsync(user, "AdOwner");

                    }
                }
                else
                {
                    return Unauthorized();
                }

                return RedirectToAction("Create", "Advertisements");
            }
            catch (Exception e)
            {
                ViewData["CountryId"] = new SelectList(db.Country.Where(a => !a.IsDeleted), "Id", "Name");
                return View("Create", advertisementOwner);
            }
        }
    }
}
