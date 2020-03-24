using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    [PetaPoco.TableName("hnInfo")]
    public class RequestData:IValidatableObject
    {
        //[Display(Name = "编号Guid")]
        //public Guid Id { get; set; }
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        [Display(Name = "身份证")]
        public string Idcard { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Phone.Length != 11 && Phone.Length != 0)
            {
                yield return new ValidationResult("电话号码必须是11位", new[] { nameof(RequestData) });
            }
            if (Regex.IsMatch(Idcard, @"^[0 - 9] * $") && Idcard.Length != 0)
            {
                yield return new ValidationResult("身份证号码必须是纯数字", new[] { nameof(RequestData) });
                if (Idcard.Length < 15 || Idcard.Length > 18)
                {
                    yield return new ValidationResult("身份证号码的位数必须是15位到18位。", new[] { nameof(RequestData) });
                }
            }

        }
    }
}
