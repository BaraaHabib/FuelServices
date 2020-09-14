using DBContext.Models;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    public class ContentManagementsController : BaseController
    {
        private readonly AirportCoreContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ContentManagementsController(AirportCoreContext context, IWebHostEnvironment hostingEnvironment)
        {
            ViewData["Title"] = "General";
            ViewData["ControllerName"] = "ContentManagements";
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/ContentManagements
        public async Task<IActionResult> Index()
        {
            ViewBag.webPath = _hostingEnvironment.WebRootPath;

            return View((await _context.ContentManagement.Where(x => !x.Name.Equals("app_name")).ToListAsync()));
        }

        // GET: Admin/ContentManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentManagement = await _context.ContentManagement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentManagement == null)
            {
                return NotFound();
            }

            return View(contentManagement);
        }

        // GET: Admin/ContentManagements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ContentManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DisplayName,IsVisible,ItemOrder,Title,Description,AnchorText,AnchorUrl,ImageUrl")] ContentManagement contentManagement, IFormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (formCollection?.Files?.Count > 0)
                    {
                        FileInfo fi = new FileInfo(formCollection.Files[0].FileName);
                        var newFilename = "P" + contentManagement.Id + "_" + string.Format("{0:d}",
                                          (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = _hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\uploads\images\ContentManagement\" + newFilename);
                        var pathToSave = @"/uploads/images/ContentManagement/" + newFilename;
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            formCollection.Files[0].CopyTo(stream);
                        }
                        contentManagement.ImageUrl = pathToSave;
                    }
                    _context.Add(contentManagement);
                    await _context.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    return RedirectToAction(nameof(Index));
                }
                return View(contentManagement);
            }
            catch (Exception e)
            {
                Message = Toast.ErrorToast(GetExceptionMessage(e));
                return View(contentManagement);
            }
        }

        // GET: Admin/ContentManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentManagement = await _context.ContentManagement.FindAsync(id);
            if (contentManagement == null)
            {
                return NotFound();
            }
            return View(contentManagement);
        }

        // POST: Admin/ContentManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContentManagement contentManagement, IFormCollection formCollection)
        {
            if (id != contentManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formCollection?.Files?.Count > 0)
                    {
                        var webPath = _hostingEnvironment.WebRootPath;
                        var pathToDelete = webPath + contentManagement.ImageUrl;
                        // TO DO: remove file
                        /*
                         *
                         */

                        // get File name
                        FileInfo fi = new FileInfo(formCollection.Files[0].FileName);
                        var newFilename = "P" + contentManagement.Id + "_" + string.Format("{0:d}",
                                          (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var path = Path.Combine("", webPath + @"\uploads\images\ContentManagement\" + newFilename);
                        var pathToSave = @"/uploads/images/ContentManagement/" + newFilename;
                        if (contentManagement.ImageUrl != pathToSave)
                        {
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                formCollection.Files[0].CopyTo(stream);
                            }
                            contentManagement.ImageUrl = pathToSave;
                        }
                    }
                    var cm = await _context.ContentManagement.AsNoTracking().FirstOrDefaultAsync(x => x.Id == contentManagement.Id);
                    contentManagement.ImageUrl = cm.ImageUrl;
                    _context.Update(contentManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentManagementExists(contentManagement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contentManagement);
        }

        [HttpPost]
        public Response<bool> DeleteConfirmed(int? id)
        {
            try
            {
                var model = _context.ContentManagement.Find(id);
                if (model == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                model.IsDeleted = true;
                var webPath = _hostingEnvironment.WebRootPath;
                var pathToDelete = webPath + model.ImageUrl;
                // TO DO: remove file
                /*
                 *
                 */

                _context.ContentManagement.Remove(model);
                _context.SaveChanges();
                Message = Toast.SucsessToast();
                return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);
            }
            catch (Exception exc)
            {
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }

        private bool ContentManagementExists(int id)
        {
            return _context.ContentManagement.Any(e => e.Id == id);
        }
    }
}