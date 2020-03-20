using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public Respones Post([FromBody] DataRequest req)
        {
            Respones respones = new Respones();
            DataOperations dataOperations = new DataOperations();
            
            try
            {
                if (req.Name != null)
                {
                    DataRequest.Instance.Name = req.Name;
                }
                if (req.IDCard != null)
                {
                    DataRequest.Instance.IDCard = req.IDCard;
                }
                if (req.PhoneNumber != null)
                {
                    DataRequest.Instance.PhoneNumber = req.PhoneNumber;
                }
                if (req.Stat != null)
                {
                    DataRequest.Instance.Stat = req.Stat;
                    dataOperations.IsType();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return Respones.Instance;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
