using ContactsAPI.Models.Mapper;
using ContactsAPI.Models.PageModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.DataRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly Contacts context;

        public PetaPoco.Database Db { get; set; }
        public ContactRepository(Contacts _context)
        {
            var ConnecStr = "server = .;database = ContactInformation;uid = sa; pwd = 123";
            var Provider = "System.Data.SqlClient";
            Db = new PetaPoco.Database(ConnecStr, Provider, null);
            context = _context;
        }
        public async Task<Contacts> AddData(ContactsDTO reg)
        {
            var soucreContacts = new Contacts();
            soucreContacts.Id = System.Guid.NewGuid();
            soucreContacts.Name = reg.Name;
            soucreContacts.Phone = reg.Phone;
            soucreContacts.IdCard = reg.IdCard;
            await Db.InsertAsync(soucreContacts);
            return soucreContacts;
        }

        public async Task<MessageRespones> DeleteData(Guid id)
        {
            var ContactNum = await Db.ExecuteScalarAsync<int>("Select Count(*) from hnInfo where id = @0", id);
            if (ContactNum <= 0) Console.WriteLine($"该ID{id}不存在");
            var Contact = await Db.DeleteAsync<Contacts>("where Id = @0", id);
            var mesage = new MessageRespones();
            if (Contact > 0)
            {
                mesage.Stat = 1;
                mesage.Mes = "删除成功";
                return mesage;
            }
            mesage.Stat = -1;
            mesage.Mes = "删除失败";
            return mesage;
        }

        //public async Task<ActionResult<IEnumerable<Contacts>>> GetData()
        //{
        //    //var contact = await Db.PageAsync<Contacts>(1, 5, "Select * from hnInfo");
        //    var contact = await Db.QueryAsync<IEnumerable<Contacts>>("Select * from hnInfo");
        //    return contact;
        //}

        public  PageInfo<Contacts> GetData(Page page)
        {
            var contact = Db.Query<Contacts>("Select * from hnInfo");
            return PageInfo<Contacts>.Create(contact, page.PageNumber, page.PageSize);
        }

        public async Task<Contacts> GetSing(Guid id)
        {
            var Contact = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);
            return Contact;
        }

        public async Task<MessageRespones> UpdateData(Guid id, ContactsDTO req)
        {
            var sql = PetaPoco.Sql.Builder.Append("set ");
            if (req.Name != null || req.Name == "") sql.Append("name = @0", req.Name);
            if (req.IdCard != null || req.IdCard == "") sql.Append("IdCard = @0", req.IdCard);
            if (req.Phone != null || req.Phone == "") sql.Append("Phone = @0", req.Phone);
            sql.Append(" where id = @0", id);
            //if (param.Id != null) sql.Append(" id = @0", param.Id);
            //if (param.Name != null || param.Name == "") sql.Append("name = @0", param.Name);
            //if (param.Phone != null || param.Phone == "") sql.Append("Phone = @0", param.Phone);
            //if (param.IdCard != null || param.IdCard == "") sql.Append("IdCard = @0", param.IdCard);



            var ContactNum = await Db.UpdateAsync<Contacts>(sql);
            //if (req.IdCard != null || req.IdCard != "") Contact.IdCard = req.IdCard;
            //if (req.Name != null || req.Name != "") Contact.Name = req.Name;
            //if (req.Phone != null || req.Phone != "") Contact.Phone = req.Phone;
            var mesage = new MessageRespones();
            if (ContactNum <= 0)
            {
                mesage.Stat = -1;
                mesage.Mes = "更新失败";
                return mesage;
            }
            mesage.Stat = 1;
            mesage.Mes = "更新成功";
            return mesage;

        }
    }
}
