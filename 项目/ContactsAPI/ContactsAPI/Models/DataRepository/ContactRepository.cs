using ContactsAPI.Models.Mapper;
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
        public PetaPoco.Database Db { get; set; }
        public ContactRepository()
        {
            var ConnecStr = "server = .;database = ContactInformation;uid = sa; pwd = 123";
            var Provider = "System.Data.SqlClient";
            Db = new PetaPoco.Database(ConnecStr, Provider, null);
        }
        public async Task<Contacts> AddData(ContactsDTO reg)
        {
            var soucreContacts = new Contacts();
            //var PhoneData = await Db.FirstAsync<string>("select phone from hnInfo where phone = @0", reg.Phone);
            //if (PhoneData == reg.Phone)
            //{
            //    Console.WriteLine($"该{reg.Phone}手机号码已存在，请重新输入");
            //    soucreContacts.Phone = reg.Phone;
            //    return soucreContacts;
            //}
            soucreContacts.Id = System.Guid.NewGuid();
            soucreContacts.Name = reg.Name;
            soucreContacts.Phone = reg.Phone;
            soucreContacts.IdCard = reg.IdCard;
            await Db.InsertAsync(soucreContacts);
            return soucreContacts;
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        //public async Task<ActionResult<IEnumerable<Contacts>>> GetData()
        //{
        //    //var contact = await Db.PageAsync<Contacts>(1, 5, "Select * from hnInfo");
        //    var contact = await Db.QueryAsync<IEnumerable<Contacts>>("Select * from hnInfo");
        //    return contact;
        //}

        public  IEnumerable<Contacts> GetData()
        {
            //var contact = await Db.PageAsync<Contacts>(1, 5, "Select * from hnInfo");
            var contact =  Db.Query<Contacts>("Select * from hnInfo");
            return contact;
        }

        public void GetSing()
        {
            throw new NotImplementedException();
        }

        public void UpdateData()
        {
            throw new NotImplementedException();
        }
    }
}
