using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace AspNetCoreApiDemo.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/PersonInfo/")]
    [ApiController]
    public class PersonInfoController : ControllerBase
    {
        //public readonly IDataRepository<RequestData> dataRepository;
        //public PersonInfoController(IDataRepository<RequestData> _dataRepository)
        //{
        //    dataRepository = _dataRepository ?? throw new ArgumentNullException(nameof(_dataRepository));
        //}

        DataRepository dataRepository = new DataRepository(new RequestData());

        

        [HttpGet]
        public IEnumerable<RequestData> Get()
        {
            
            return dataRepository.GetRequestDatas();
        }
        [HttpGet("item/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var UserInfo = await dataRepository.GetRequestDatas(id);
            return new JsonResult(UserInfo);
        }



        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.AddData(reg);
            return new JsonResult(UserInfo);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.UpdateData(reg, id);
            return new JsonResult(UserInfo);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IEnumerable<RequestData>> Delete(Guid id)
        {
            return await dataRepository.DeleteData(id);
        }
    }
}