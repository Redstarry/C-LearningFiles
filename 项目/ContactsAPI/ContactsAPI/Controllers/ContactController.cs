using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
using ContactsAPI.Models.DataRepository;
using Microsoft.Extensions.Configuration;
using ContactsAPI.Models.PageModel;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using ContactsAPI.Models.config;
using Microsoft.AspNetCore.Cors;
using ContactsAPI.Models.LoginInfo;
using Microsoft.AspNetCore.Authorization;
using ContactsAPI.Models.HangfireInfo;

namespace ContactsAPI.Controllers
{
    //[Authorize]
    [EnableCors("Domain")]
    [Route("v1/Contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ContactController(IContactRepository _contactRepository, AutoMapper.IMapper mapper)
        {
            contactRepository = _contactRepository ?? throw new ArgumentNullException(nameof(_contactRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        
        /// <summary>
        /// 查询全部的数据
        /// </summary>
        /// <param name="page">里面包含pageSize(一页的数量)和pageNumber(查询的页数)</param>
        /// <returns></returns>
        
        [HttpGet(Name = nameof(Get))]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] Page page)
        {
            //var ContactDTO = _mapper.Map<IEnumerable<ContactsDTO>>(Contact);
            //await Task.Delay(10);
            //return new JsonResult(ContactDTO);
            var data = await contactRepository.GetData(page);
            var nextLink = data.HasNext ? CreateContactsResourceUri(page,ResourceUriType.NextPage) : null;
            var PreviousLink = data.HasPrevious ? CreateContactsResourceUri(page, ResourceUriType.PreviousPage) : null;

            var result = new {
                nextLink,
                PreviousLink,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                TotalCount = data.TotalCount,
                data
            };
            return Ok(result);
        }
        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="id">查询数据的Guid</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            //var ContactDTO = _mapper.Map<ContactsDTO>(Contact);
            //return new JsonResult(ContactDTO);
            return Ok(await contactRepository.GetSingle(id));
        }
        /// <summary>
        /// 根据 姓名，电话号码， 身份证 进行查询
        /// </summary>
        /// <param name="reg">里面包含姓名，电话号码， 身份证 </param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("propselect")]
        public async Task<IActionResult> PostByPropGetInfo([FromBody]ContactsDTO reg)
        {
            return Ok(await contactRepository.Get(reg));
        }
        /// <summary>
        /// 登录验证接口
        /// </summary>
        /// <param name="userInfo">用户名和密码</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(UserInfo userInfo)
        {
            return Ok(await contactRepository.UserInfo(userInfo));
        }
        #region
        ///// <summary>
        ///// 创建定时任务
        ///// </summary>
        ///// <param name="RequestInfo"></param>
        ///// <returns></returns>
        //[HttpPost("TimedTasks")]
        //public async Task<IActionResult> TimedTasks(OperationHangFire RequestInfo)
        //{
        //    return Ok(await contactRepository.StartTask(RequestInfo));
        //}
        ///// <summary>
        ///// 创建队列任务
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("FireAndForgetJobs")]
        //public async Task<IActionResult> FireAndForgetJobs()
        //{
        //    return Ok(await contactRepository.FireAndForgetJobs());
        //}
        #endregion
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="reg">添加的详细数据</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactsDTO reg)
        {
            yanz rules = new yanz();
            var Result = rules.Validate(reg);

            if (!Result.IsValid)
            {
                return Ok(new ResultDTO(200, Result.ToString(), "", ResultStatus.Error));
            }
            return Ok(await contactRepository.AddData(reg));
        }
        /// <summary>
        /// 根据ID更新数据
        /// </summary>
        /// <param name="id">更新的数据ID</param>
        /// <param name="req">更新后的数据内容</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id ,[FromBody] ContactsDTO req)
        {
            return Ok(await contactRepository.UpdateData(id,req));
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="id">要删除数据的ID</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await contactRepository.DeleteData(id));
        }

        private string CreateContactsResourceUri(Page parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    var b = Url.Link(nameof(Get), new
                    {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize
                    });
                    return b;
                case ResourceUriType.NextPage:
                    var a = Url.Link(nameof(Get), new
                    {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize
                    });
                    return a;
                default:
                    return Url.Link(nameof(Get), new
                    {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize
                    });
            }
        }
    }
}