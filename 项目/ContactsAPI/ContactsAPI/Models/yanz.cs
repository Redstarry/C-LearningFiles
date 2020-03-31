using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;

namespace ContactsAPI.Models
{
    public class yanz:AbstractValidator<ContactsDTO>
    {
        public yanz()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Phone).NotEmpty().WithMessage("电话号码为空").Length(11).WithMessage("电话号码长度必须是11位");
            RuleFor(p => p.IdCard).NotEmpty().Must(JudgeIdCardnumber).WithMessage("身份证号位必须是15位或18位或格式不正确"); 

        }

        private bool JudgeIdCardnumber(ContactsDTO contactsDTO, string arg)
        {
            var rx = new Regex(@"\d{17}[\d|x]|\d{15}");
            return (contactsDTO.IdCard.Length == 15 || contactsDTO.IdCard.Length == 18) && rx.IsMatch(contactsDTO.IdCard);  
        }

        private bool judgePhone(ContactsDTO contactsDTO, string length)
        {
            return contactsDTO.Phone.Length != 11;
        }

        public MessageRespones Suceess()
        {
            var message = new MessageRespones();
            message.Stat = 1;
            message.Mes = "更新成功";
            return message;
        }

        public MessageRespones Fail()
        {
            var message = new MessageRespones();
            message.Stat = -1;
            message.Mes = "更新失败";
            return message;
        }
    }
}
