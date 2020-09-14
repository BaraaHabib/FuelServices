using AutoMapper;
using DBContext.Models;
using FuelServices.DBContext.Domain;
using FuelServices.Site.Helpers.Toast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class ContactsController : BaseController
    {
        public ContactsController(AirportCoreContext context, IServiceProvider provider) : base(context, provider)
        {
        }

        /// <summary>
        /// get contacts
        /// </summary>
        /// <returns>Basic contacts for supplier</returns>
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var supplierId = user.FuelSupplier.Id;

            var contacts = db.SupplierContact
                .Where(sc => !sc.IsDeleted)
                .Where(sc => sc.FuelSupplierId == supplierId)
                .Select(x => new ContactRecordModel()
                {
                    Id = x.Id,
                    FuelSupplierId = supplierId,
                    ContactId = (int)x.ContactId,
                    Contact = x.Contact,
                    ItemOrder = x.ItemOrder,
                    Value = x.Value
                }).ToList();

            return View(contacts);
        }

        public IActionResult Create()
        {
            // get contact types
            ViewBag.Contacts = new SelectList(db.Contact.Where(x => !x.IsDeleted).Select(x => new { x.Id, x.DisplayName }), "Id", "DisplayName");

            try
            {
                var contact = new ContactRecordModel();
                return View(contact);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Contacts.AddContact");
                return View(new ContactRecordModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactRecordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await GetCurrentUserAsync();
                    var supplierId = user.FuelSupplier.Id;

                    SupplierContact contact = new SupplierContact()
                    {
                        FuelSupplierId = supplierId,
                        ContactId = model.ContactId,
                        Value = model.Value,
                        ItemOrder = model.ItemOrder,
                        Description = model.Description
                    };
                    db.Add(contact);
                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    return RedirectToAction("Index");
                }
                ViewBag.Contacts = new SelectList(db.Contact.Where(x => !x.IsDeleted).Select(x => new { x.Id, x.DisplayName })
                    , "Id", "DisplayName", model?.Id);
                return View(model);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Contacts.Create");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var contact = await db.SupplierContact.FindAsync(id);
                if (contact is null)
                {
                    return NotFound();
                }

                // get contact types

                ViewBag.Contacts = new SelectList(db.Contact.Where(x => !x.IsDeleted).Select(x => new { x.Id, x.DisplayName })
                    , "Id", "DisplayName", id);
                var model = GetService<IMapper>().Map<ContactRecordModel>(contact)
                    ;
                return View(model);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, "Contacts.Edit");
                Message = Toast.ErrorToast(GetExceptionMessage(e));
                return View(new ContactRecordModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactRecordModel model, int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contact = await db.SupplierContact.FindAsync(id);
                    var user = await GetCurrentUserAsync();
                    var supplierId = user.FuelSupplier.Id;

                    contact.Value = model.Value;
                    contact.ItemOrder = model.ItemOrder;
                    contact.Description = model.Description;

                    db.Update(contact);
                    await db.SaveChangesAsync();
                    Message = Toast.SucsessToast();
                    return RedirectToAction("Index");
                }
                ViewBag.Contacts = db.Contact.Where(x => !x.IsDeleted).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Contacts.Create");
                Message = Toast.ErrorToast(GetExceptionMessage(ex));
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var con = await db.SupplierContact.FindAsync(id);

            if (con is null)
                return NotFound();

            var model = GetService<IMapper>().Map<ContactRecordModel>(con);

            return View(model);
        }

        [HttpPost]
        public Response<bool> DeleteConfirmed(int? id)
        {
            try
            {
                var model = db.SupplierContact.Find(id);
                if (model == null)
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                model.IsDeleted = true;

                db.Remove(model);
                db.SaveChanges();
                Message = Toast.SucsessToast();
                return new Response<bool>(Constants.SUCCESS_CODE, true, Constants.SUCCESS);
            }
            catch (Exception exc)
            {
                Serilog.Log.Error(exc, "Contacts.DeleteConfirmed");
                Message = Toast.ErrorToast(GetExceptionMessage(exc));
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(exc));
            }
        }

        private bool RequestExists(int id)
        {
            return db.Request.Any(e => e.Id == id);
        }
    }
}