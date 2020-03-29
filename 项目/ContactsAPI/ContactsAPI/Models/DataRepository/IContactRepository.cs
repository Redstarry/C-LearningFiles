using ContactsAPI.Models.PageModel;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public interface IContactRepository
    {
        //Task<ActionResult<Page<Contacts>>> GetData();
        PageInfo<Contacts> GetData(Page page);
        IEnumerable<Contacts> Get(ContactsDTO reg);
        Task<Contacts> AddData(ContactsDTO reg);
        Task<MessageRespones> UpdateData(Guid id, ContactsDTO req);
        Task<Contacts> GetSing(Guid id);
        Task<MessageRespones> DeleteData(Guid id);
    }
}
