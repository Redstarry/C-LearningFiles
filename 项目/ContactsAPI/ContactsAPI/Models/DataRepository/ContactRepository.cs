using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.DataRepository
{
    public class ContactRepository : IContactRepository
    {
        //private readonly IConfiguration _configuration;
        public PetaPoco.Database Db { get; set; }
        public ContactRepository(IConfiguration configuration)
        {
            //_configuration = configuration;
            var ConnecStr = configuration["ConnectionStrings:ConnectionStr"];
            var Provider = configuration["ConnectionStrings:Priovder"];
            Db = new PetaPoco.Database(ConnecStr, Provider, null);
        }
        public void AddData()
        {
            throw new NotImplementedException();
        }

        public void DeleteData()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Page<Contacts>>> GetData()
        {
            var contact = await Db.PageAsync<Contacts>(1, 5, "Select * from hnInfo");
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
