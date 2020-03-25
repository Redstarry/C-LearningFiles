using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ContactsAPI.Models
{
    public class yanz:AbstractValidator<ContactsDTO>
    {
        public yanz()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Phone).NotEmpty().Length(11).WithMessage("电话号码为空，或长度必须11位，或已用该号码");
            RuleFor(p => p.IdCard).NotEmpty().Length(15, 18).WithMessage("身份证号码必须是15 到 18位");
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
