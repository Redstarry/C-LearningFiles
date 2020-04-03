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
        Task<MessageRespones> TextPostInfo(ContactsDTO contactsDTO);
        Task<MessageRespones> TextPutInfo(ContactsDTO contactsDTO, string id);

        Task<MessageRespones> TextDeleteInfo(string id);

        Task<IEnumerable<ContactsDTO>> TextGetAllInfo(int pageSize, int pageNumber);

        Task<ContactsDTO> TextGetSingle(string id);

        Task<IEnumerable<ContactsDTO>> TextGetByPropInfo(ContactsDTO contactsDTO);
    }
}
