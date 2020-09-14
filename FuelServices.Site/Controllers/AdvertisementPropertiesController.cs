using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Site.Controllers
{
    public class AdvertisementPropertiesController : BaseController
    {

        public AdvertisementPropertiesController(AirportCoreContext context) : base(context)
        {
        }

        // GET: AdvertisementProperties/Create
        public IActionResult Create(int id)
        {
            Advertisement advertisement = db.Advertisement.Find(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            var Properties = db.AdvertisementTypeProperty.Where(p => !p.IsDeleted /*&&
                (p.AdvertisementTypeId == null || p.AdvertisementTypeId == advertisement.AdvertisementTypeId) &&
                p.ExceptAdvertisementType1Id != advertisement.AdvertisementTypeId &&
                p.ExceptAdvertisementType2Id != advertisement.AdvertisementTypeId*/).ToList();
            AdvertisementProperty[] advertisementProperties = new AdvertisementProperty[Properties.Count()];
            for (int i = 0; i < Properties.Count(); i++)
            {
                AdvertisementProperty temp = new AdvertisementProperty();
                temp.AdvertisementId = id;
                temp.AdvertisementTypePropertyId = Properties.ElementAt(i).Id;
                temp.AdvertisementTypeProperty = Properties.ElementAt(i);
                if (Properties.ElementAt(i).Unit != null)
                {
                    ViewData["unit_options_" + i] = new SelectList(Properties.ElementAt(i).Unit.Split(",").ToList());

                }

                if (Properties.ElementAt(i).Options != null)
                {
                    JObject json = JObject.Parse(Properties.ElementAt(i).Options);
                    List<PropertyChoice> selectListItems = new List<PropertyChoice>();
                    foreach (var item in json)
                    {
                        selectListItems.Add(new PropertyChoice(item.Value.ToString(), item.Key));
                    }
                    ViewData["options_" + i] = new SelectList(selectListItems, "Name", "Value");
                }
                advertisementProperties[i] = temp;
            }
            return View(advertisementProperties);
        }

        // POST: AdvertisementProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> PostCreateAsync(AdvertisementProperty[] advertisementProperty)
        {
            try
            {
                foreach (var item in advertisementProperty)
                {
                    item.Advertisement = null;
                    db.Add(item);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "AdvertisementProperties", advertisementProperty);
            }
        }
    }

    public class PropertyChoice
    {
        public PropertyChoice(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
