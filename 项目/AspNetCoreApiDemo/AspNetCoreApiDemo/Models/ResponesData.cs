using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiDemo.Models
{
    [PetaPoco.TableName("hnInfo")]
    public class ResponesData
    {
        [Display(Name = "编号Guid")]
        public Guid Id { get; set; }
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        [Display(Name = "身份证")]
        public string Idcard { get; set; }
    }
}
