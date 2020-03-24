using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiDemo.Data;
using AspNetCoreApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
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

        DataRepository dataRepository = new DataRepository(new ResponesData());

        

        [HttpGet]
        //[FromQuery] 有这个可以在url后面筛选parameters里面的属性。
        //http://localhost:5000/api/personInfo?pageNumber=2&pagesize=5
        //public Page<RequestData> Get([FromQuery] parameters)
        //{
        //    var Data = dataRepository.GetRequestDatas();
        //    return Data;
        //}
        public ActionResult<Page<RequestData>> Get()
        {
            var Data = dataRepository.GetRequestDatas();
            return new JsonResult(Data); 
        }
        [HttpGet("item/{id}")]
        public async Task<ActionResult<IEnumerable<RequestData>>> Get(Guid id)
        {
            var UserInfo = await dataRepository.GetRequestDatas(id);
            return new JsonResult(UserInfo);
        }



        [HttpPost("add")]
        public async Task<ActionResult<IEnumerable<RequestData>>> Post([FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.AddData(reg);
            return new JsonResult(UserInfo);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult<IEnumerable<RequestData>>> Put(Guid id, [FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.UpdateData(reg, id);
            return new JsonResult(UserInfo);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<IEnumerable<RequestData>>> Delete(Guid id)
        {
            return new JsonResult(await dataRepository.DeleteData(id));
        }
    }
}