using ContactsAPI.Models.config;
using ContactsAPI.Models.Mapper;
using ContactsAPI.Models.PageModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContactsAPI.Models.DataRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AutoMapper.IMapper _mapper;

        public PetaPoco.Database Db { get; set; }
        public string ConnecStr { get; set; }
        public string Provider { get; set; }
        public ContactRepository(IOptions<ConnectionConfig> options, AutoMapper.IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ConnecStr = options.Value.ConnectionStr;
            Provider = options.Value.Priovder;
            Db = new PetaPoco.Database(ConnecStr, Provider, null);
            
        }
        public async Task<bool> AddData(ContactsDTO reg)
        {
            var soucreContacts = _mapper.Map<Contacts>(reg);
            soucreContacts.Id = System.Guid.NewGuid();
      
            if (Convert.ToInt32(await Db.InsertAsync(soucreContacts)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<MessageRespones> DeleteData(Guid id)
        {
            var ContactNum = await Db.ExecuteScalarAsync<int>("Select Count(*) from hnInfo where id = @0", id);
            if (ContactNum <= 0) Console.WriteLine($"该ID{id}不存在");
            var Contact = await Db.DeleteAsync<Contacts>("where Id = @0", id);
            var mesage = new MessageRespones();
            if (Contact > 0)
            {
                mesage.Stat = 1;
                mesage.Mes = "删除成功";
                return mesage;
            }
            mesage.Stat = -1;
            mesage.Mes = "删除失败";
            return mesage;
        }

        public  PageInfo<Contacts> GetData(Page page)
        {
            var contact = Db.Query<Contacts>("Select * from hnInfo");
            return PageInfo<Contacts>.Create(contact, page.PageNumber, page.PageSize);
        }
        public  IEnumerable<Contacts> Get(ContactsDTO reg)
        {
            var sql = PetaPoco.Sql.Builder
                .Select("*")
                .From("hnInfo");
            if (reg.Id != null && reg.Id.ToString() != "" && reg.Id != Guid.Empty) sql.Where("id = @0",reg.Id);
            if (reg.Name != null && reg.Name != "") sql.Where("name=@0",reg.Name);
            if (reg.Phone != null && reg.Phone != "") sql.Where("Phone=@0", reg.Phone);
            if (reg.IdCard != null && reg.IdCard != "") sql.Where("IdCard=@0", reg.IdCard);
            var Contact = Db.Query<Contacts>(sql);
            return Contact;


        }
        public async Task<Contacts> GetSing(Guid id)
        {
            var Contact = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);
            return Contact;
        }

        public async Task<MessageRespones> UpdateData(Guid id, ContactsDTO req)
        {
            var mesage = new MessageRespones();
            if (await Db.SingleOrDefaultAsync<Contacts>("where id = @0", id) == null)
            {
                mesage.Stat = -1;
                mesage.Mes = "修改的数据不存在";
                return mesage;
            }
            var sql = PetaPoco.Sql.Builder.Append("set ");
            if (req.Name != null && req.Name != "") sql.Append("name = @0 ", req.Name);
            if (req.IdCard != null && req.IdCard != "") sql.Append(", IdCard = @0 ", req.IdCard);
            if (req.Phone != null && req.Phone != "") sql.Append(", Phone = @0 ", req.Phone);
            sql.Append(" where id = @0", id);
            var ContactNum = await Db.UpdateAsync<Contacts>(sql);
            
            if (ContactNum <= 0)
            {
                mesage.Stat = -1;
                mesage.Mes = "更新失败";
                return mesage;
            }
            mesage.Stat = 1;
            mesage.Mes = "更新成功";
            return mesage;

        }
    }
}
