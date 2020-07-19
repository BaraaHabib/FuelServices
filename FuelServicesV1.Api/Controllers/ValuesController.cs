using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<string>> Values()
        {
            return new string[] { "value1", "value2" };
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
