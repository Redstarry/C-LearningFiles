using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api/PersonInfo")]
    public class PersonInfoController : ControllerBase
    {
        public readonly IDataRepository<RequestData> dataRepository;
        public PersonInfoController(IDataRepository<RequestData> _dataRepository)
        {
            dataRepository = _dataRepository ?? throw new ArgumentNullException(nameof(_dataRepository));
        }

        [HttpGet]
        public IEnumerable<RequestData> Get()
        {

            return dataRepository.GetRequestDatas();
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var UserInfo = await dataRepository.GetRequestDatas(id);
            return new JsonResult(UserInfo);
        }
        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.AddData(reg);
            return new JsonResult(UserInfo);
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.UpdateData(reg, id);
            return new JsonResult(UserInfo);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IEnumerable<RequestData>> Delete(Guid id)
        {
            return await dataRepository.DeleteData(id);
        }
    }
}