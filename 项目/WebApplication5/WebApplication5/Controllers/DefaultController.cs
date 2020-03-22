using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // GET: api/Default
        //[HttpGet]
        public IEnumerable<RequestData> Get()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var Data = db.Query<RequestData>("select * from hnInfo");
            return Data;
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<RequestData> Get(Guid id)
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var Data = await db.SingleOrDefaultAsync<RequestData>("where Id = @0", id);
            return Data;
        }
        //[HttpGet("{Name}", Name = "GetName")]
        //public Task<RequestData> GetName(string Name)
        //{
        //    string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
        //    var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
        //    var Data = db.SingleOrDefaultAsync<RequestData>("where Name = @0", Name);
        //    return Data;
        //}

        // POST: api/Default
        [HttpPost]
        public async Task<RequestData> Post([FromBody] RequestData reg)
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var requestData = new RequestData();
            requestData.Id = System.Guid.NewGuid();
            requestData.Name = reg.Name;
            requestData.Phone = reg.Phone;
            requestData.Idcard = reg.Idcard;
            await db.InsertAsync(requestData);
            return requestData;
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public  async Task<RequestData> Put(Guid id, [FromBody] RequestData reg)
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var Data = await db.SingleOrDefaultAsync<RequestData>("where Id = @0", id);
            var requestData = new RequestData();
            if (reg.Name == null || reg.Name == "") requestData.Name = Data.Name;
            else requestData.Name = reg.Name;
            if (reg.Phone == null || reg.Phone == "") requestData.Phone = Data.Phone;
            else requestData.Phone = reg.Phone;
            if (reg.Idcard == null || reg.Idcard == "") requestData.Idcard = Data.Idcard;
            else requestData.Idcard = reg.Idcard;
            requestData.Id = Data.Id;
            int Result = await db.UpdateAsync(requestData);
            
            return requestData;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IEnumerable<RequestData>> Delete(Guid id)
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            await db.DeleteAsync<RequestData>("where Id = @0",id);
            var Data = db.Query<RequestData>("select * from hnInfo");
            return Data;
        }
    }
}
