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
        DataRepository dataRepository = new DataRepository(new ResponesData());

        
        /// <summary>
        /// 查询全部的API方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[FromQuery] 有这个可以在url后面筛选parameters里面的属性。
        //http://localhost:5000/api/personInfo?pageNumber=2&pagesize=5
        //public Page<RequestData> Get([FromQuery] parameters)
        //{
        //    var Data = dataRepository.GetRequestDatas();
        //    return Data;
        //}
        public ActionResult<Page<ResponesData>> Get()
        {
            var Data = dataRepository.GetRequestDatas();
            return new JsonResult(Data); 
        }
        /// <summary>
        /// 通过Id查询的API方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("item/{id}")]
        public async Task<ActionResult<IEnumerable<ResponesData>>> Get(Guid id)
        {
            var UserInfo = await dataRepository.GetRequestDatas(id);
            return new JsonResult(UserInfo);
        }



        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<IEnumerable<ResponesData>>> Post([FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.AddData(reg);
            return new JsonResult(UserInfo);
        }

        /// <summary>
        /// 更新的API
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<IEnumerable<ResponesData>>> Put(Guid id, [FromBody] RequestData reg)
        {
            var UserInfo = await dataRepository.UpdateData(reg, id);
            return new JsonResult(UserInfo);
        }
        /// <summary>
        /// 删除的API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> Delete(Guid id)
        {
            return await dataRepository.DeleteData(id);
        }
        //public async Task<ActionResult<IEnumerable<ResponesData>>> Delete(Guid id)
        //{
        //    return new JsonResult(await dataRepository.DeleteData(id));
        //}
    }
}