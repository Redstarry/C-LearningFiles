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

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        //private readonly IContactRepository _ContactRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ContactController(AutoMapper.IMapper mapper)
        {
            //_ContactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            

        }
        ContactRepository contactRepository = new ContactRepository();
        [HttpGet]
        //public  async Task<ActionResult<Page<ContactsDTO>>> Get()
        //{
        //    var Contact = await contactRepository.GetData();
        //    var ContactDTO = _mapper.Map<Page<ContactsDTO>>(Contact);
        //    return ContactDTO;
        //}

        public  IEnumerable<ContactsDTO> Get()
        {
            var Contact = contactRepository.GetData();
            var ContactDTO = _mapper.Map<IEnumerable<ContactsDTO>>(Contact);
            return ContactDTO;
        }

        [HttpGet("id")]
        public async Task Get(Guid id)
        { 
        
        }

        [HttpPost("add")]
        public async Task<ActionResult<ContactsDTO>> Post([FromBody] ContactsDTO reg)
        {
            var Contact = await contactRepository.AddData(reg);
            var ContactDTO = _mapper.Map<ContactsDTO>(Contact);
            return ContactDTO;
        }

        [HttpPut("update")]
        public async Task Put()
        { 
        
        }

        [HttpDelete("delete")]
        public async Task Delete()
        { 
            
        }
    }
}