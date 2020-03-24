using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    [PetaPoco.TableName("hnInfo")]
    public class ContactsDTO
    {
        public Guid Id { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactIdCard { get; set; }
    }
}
