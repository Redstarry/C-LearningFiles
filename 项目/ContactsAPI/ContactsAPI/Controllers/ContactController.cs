using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IContactRepository _ContactRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ContactController(IContactRepository contactRepository, AutoMapper.IMapper mapper)
        {
            _ContactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public  async Task<ActionResult<Page<ContactsDTO>>> Get()
        {
            var Contact = await _ContactRepository.GetData();
            var ContactDTO = _mapper.Map<Page<ContactsDTO>>(Contact);
            return ContactDTO;
        }

        [HttpGet("id")]
        public async Task Get(Guid id)
        { 
        
        }

        [HttpPost("add")]
        public async Task Post()
        { 
        
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