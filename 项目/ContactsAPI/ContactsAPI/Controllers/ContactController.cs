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

namespace ContactsAPI.Controllers
{
    [EnableCors("Domain")]
    [Route("api/[controller]")]
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
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(Get))]
        public async Task<IActionResult> Get([FromQuery] Page page)
        {
            var Contact = contactRepository.GetData(page);
            var ContactDTO = _mapper.Map<IEnumerable<ContactsDTO>>(Contact);
            await Task.Delay(10);  
            return new JsonResult(ContactDTO);
        }
        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Contact = await contactRepository.GetSing(id);
            var ContactDTO = _mapper.Map<ContactsDTO>(Contact);
            return new JsonResult(ContactDTO);
        }
        [HttpGet("compound")]
        public async Task<IActionResult> GetCompound([FromQuery] ContactsDTO reg)
        {
            var Contact = contactRepository.Get(reg);
            var ContactDTO = _mapper.Map<IEnumerable<ContactsDTO>>(Contact);
            await Task.Delay(10);
            return new JsonResult(ContactDTO);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] ContactsDTO reg)
        {
            yanz rules = new yanz();
            var mesage = new MessageRespones();
            var Result = rules.Validate(reg);

            if (!Result.IsValid)
            {
                mesage.Stat = -1;
                mesage.Mes = Result.ToString();
                return new JsonResult(mesage);
            }
            var Contact = await contactRepository.AddData(reg);
            var ContactDTO = _mapper.Map<ContactsDTO>(Contact);
            mesage.Stat = 1;
            mesage.Mes = "添加成功";
            return new JsonResult(mesage);
        }
        /// <summary>
        /// 根据ID更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<MessageRespones> Put(Guid id, [FromBody] ContactsDTO req)
        {
            return await contactRepository.UpdateData(id, req); 
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<MessageRespones> Delete(Guid id)
        {
            return await contactRepository.DeleteData(id); 
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