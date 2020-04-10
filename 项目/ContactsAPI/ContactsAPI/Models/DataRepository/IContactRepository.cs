using ContactsAPI.Models.LoginInfo;
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
        Task<PageInfo<Contacts>> GetData(Page page);
        Task<ResultDTO> Get(ContactsDTO reg);
        Task<ResultDTO> AddData(ContactsDTO reg);
        Task<ResultDTO> UpdateData(Guid id ,ContactsDTO req);
        Task<ResultDTO> GetSingle(Guid id);
        Task<ResultDTO> DeleteData(Guid id);

        Task<ResultDTO> UserInfo(UserInfo userInfo);
    }
}
