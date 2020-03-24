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
        Task<ActionResult<Page<Contacts>>> GetData();
        void AddData();
        void UpdateData();
        void GetSing();
        void DeleteData();
    }
}
