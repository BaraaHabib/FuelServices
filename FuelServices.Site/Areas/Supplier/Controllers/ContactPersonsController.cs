using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.Domain;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    [Display(Name ="Contact Persons")]
    public class ContactPersonsController : BaseController
    {
        public ContactPersonsController(AirportCoreContext context, IServiceProvider provider) : base(context, provider)
        {
        }

        /// <summary>
        /// get contacts
        /// </summary>
        /// <returns>Basic contacts for supplier</returns>
        public async Task<IActionResult> Index()
        {
            //AutoMapper.Mapper.AssertConfigurationIsValid();
            var user = await GetCurrentUserAsync();
            var supplier = user.FuelSupplier;

            var contacts = supplier.SupplierContactPerson
                .Where(sc => !sc.IsDeleted).ToList();
            //GetService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
            var result = GetService<IMapper>().Map<List<SupplierContactPersonRecordModel>>(contacts);

            return View(result);
        }

        public IActionResult Create()
        {
            // get contact types
            ViewBag.Contacts = db.Contact.Where(x => !x.IsDeleted).ToList();

            try
            {
                var contact = new SupplierContactPersonRecordModel();
                return View(contact);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "ContactPersons.Create");
                Message = Toast.ErrorToast(GetExceptionMessage(e));
                return View(new SupplierContactPersonRecordModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierContactPersonRecordModel model)
        {
            ViewBag.Contacts = db.Contact.Where(x => !x.IsDeleted).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await GetCurrentUserAsync();
                    var supplierId = user.FuelSupplier.Id;

                    var cp = new SupplierContactPerson()
                    {
                        FuelSupplierId = supplierId,
                        ItemOrder = model.ItemOrder,
                        JobTitle = model.JobTitle,
                        Name = model.Name
                    };
                    await db.AddAsync(cp);

                    await db.SaveChangesAsync();
                    List<SupplierContactPersonContact> vals = new List<SupplierContactPersonContact>();
                    model.Values.ForEach(v =>
                    {
                        vals.Add(new SupplierContactPersonContact()
                        {
                            SupplierContactPersonId = cp.Id,
                            ContactId = v.ContactId,
                            Value = v.Value,
                            Description = v.Description
                        });
                    });
                    await db.AddRangeAsync(vals);
                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "ContactPersons.Create");
                Message = Toast.ErrorToast(GetExceptionMessage(ex));
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            // get contact types
            ViewBag.Contacts = db.Contact.Where(x => !x.IsDeleted).ToList();

            try
            {
                var contact = await db.SupplierContactPerson.FindAsync(id);
                if (contact is null)
                {
                    return NotFound();
                }

                var model = GetService<IMapper>().Map<SupplierContactPersonRecordModel>(contact)
                    ;
                return View(model);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "ContactPersons.Edit");
                Message = Toast.ErrorToast(GetExceptionMessage(e));
                return View(new ContactRecordModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierContactPersonRecordModel model, int? id)
        {
            ViewBag.Contacts = db.Contact.Where(x => !x.IsDeleted).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    var contactPerson = await db.SupplierContactPerson.FindAsync(id);
                    var user = await GetCurrentUserAsync();
                    var supplierId = user.FuelSupplier.Id;

                    contactPerson.Name = model.Name;
                    contactPerson.JobTitle = model.JobTitle;
                    contactPerson.ItemOrder = model.ItemOrder;

                    db.Update(contactPerson);

                    db.RemoveRange(contactPerson.SupplierContactPersonContact);

                    var contactPersons = new List<SupplierContactPersonContact>();
                    model.Values.ForEach(c =>
                    {
                        contactPersons.Add(new SupplierContactPersonContact()
                        {
                            ContactId = c.ContactId,
                            SupplierContactPersonId = contactPerson.Id,
                            Value = c.Value,
                            Description = c.Description
                        });
                    });
                    await db.AddRangeAsync(contactPersons);
                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "ContactPersons.Edit");
                Message = Toast.ErrorToast(GetExceptionMessage(ex));
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var con = await db.SupplierContactPerson.FindAsync(id);

            if (con is null)
                return NotFound();

            var model = GetService<IMapper>().Map<SupplierContactPersonRecordModel>(con);

            return View(model);
        }

        [HttpPost]
        public async Task<Response<bool>> DeleteConfirmed(int? id)
        {
            try
            {
                var suppContact = await db.SupplierContactPerson.FindAsync(id);

                if (suppContact == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);

                db.SupplierContactPersonContact.RemoveRange(suppContact.SupplierContactPersonContact);
                db.SupplierContactPerson.Remove(suppContact);
               var res=  await db.SaveChangesAsync();
                Message = Toast.SucsessToast();
                return new Response<bool>(Constants.SUCCESS_CODE, true,"" +res);
            }
            catch (Exception exc)
            {
                Serilog.Log.Error(exc, "ContactPersons.DeleteConfirmed");
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }

        private bool RequestExists(int id)
        {
            return db.Request.Any(e => e.Id == id);
        }
    }
}