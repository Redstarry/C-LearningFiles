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
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码 11位号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 身份证号 15位或18位，可以带X
        /// </summary>
        public string IdCard { get; set; }
    }
    
}
