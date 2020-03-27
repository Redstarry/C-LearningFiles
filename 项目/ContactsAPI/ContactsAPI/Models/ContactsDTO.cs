using ContactsAPI.Models.config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
    }
    
}
