using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class ValuesController : BaseController
    {
        public ValuesController(IServiceProvider provider) : base(provider)
        {
        }

        // GET api/values
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<int>>> Values()
        {
            int rows1 = 0;
            int rows2 = 0;
            var user = await GetCurrentUserAsync();
            foreach (var item in user.Customer.Request)
            {
                db.RemoveRange(item.RequestOffers);
                rows1 += db.SaveChanges();
            }
            db.RemoveRange(user.Customer.Request);
            rows2 += db.SaveChanges();
            return new int[] { rows1, rows2};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}