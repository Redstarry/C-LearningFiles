using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.Mapper
{
    public class ContactsMapper:Profile
    {
        public ContactsMapper()
        {
            //CreateMap<Contacts, ContactsDTO>().ForMember(target=>target.ContactName, opt => opt.MapFrom(src => src.Name));
            //CreateMap<Contacts, ContactsDTO>().ForMember(target => target.ContactPhone, opt => opt.MapFrom(src => src.Phone));
            //CreateMap<Contacts, ContactsDTO>().ForMember(target => target.ContactIdCard, opt => opt.MapFrom(src => src.IdCard));
            CreateMap<Contacts, ContactsDTO>();
        }
    }
}
