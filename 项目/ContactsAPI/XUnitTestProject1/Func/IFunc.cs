using ContactsAPI.Models;
using ContactsAPI.Models.PageModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestProject1.Func
{
    public interface IFunc
    {
        Task<ResultDTO> TextPostInfo(ContactsDTO contactsDTO);
        Task<ResultDTO> TextPutInfo(ContactsDTO contactsDTO, string id);

        Task<ResultDTO> TextDeleteInfo(string id);

        Task<ResultDTO> TextGetAllInfo(int pageSize, int pageNumber);

        Task<ResultDTO> TextGetSingle(string id);

        Task<ResultDTO> TextGetByPropInfo(ContactsDTO contactsDTO);
    }
}
