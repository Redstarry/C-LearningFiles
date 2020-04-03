using ContactsAPI.Models.config;
using ContactsAPI.Models.Mapper;
using ContactsAPI.Models.PageModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PetaPoco;
using System;
using System.Collections;
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
            //Db = new PetaPoco.Database(ConnecStr, Provider, null);
            Db = new PetaPoco.Database("server = .;database = ContactInformation;uid = sa; pwd = 123", "System.Data.SqlClient", null);

        }
        public async Task<ResultDTO> AddData(ContactsDTO reg)
        {
            var soucreContacts = _mapper.Map<Contacts>(reg);
            soucreContacts.Id = System.Guid.NewGuid();
            if (await Db.InsertAsync(soucreContacts) != null)
            {
                var result = _mapper.Map<ContactsDTO>(soucreContacts);
                return new ResultDTO(200, "添加成功", soucreContacts, ResultStatus.Suceess); 
            }
            else
            {
                return new ResultDTO(200, "添加失败", reg, ResultStatus.Fail);
            }
            
        }

        public async Task<ResultDTO> DeleteData(Guid id)
        {
            var ContactNum = await Db.ExecuteScalarAsync<int>("Select Count(*) from hnInfo where id = @0", id);
            if (ContactNum <= 0) Console.WriteLine($"该ID{id}不存在");
            var Contact = await Db.DeleteAsync<Contacts>("where Id = @0", id);
            var mesage = new MessageRespones();
            if (Contact > 0)
            {
                return new ResultDTO(200, "删除成功", "", ResultStatus.Suceess);
            }
            return new ResultDTO(200, "删除失败", "", ResultStatus.Fail);
        }

        public async Task<ResultDTO> GetData(Page page)
        {
            var contact = Db.Query<Contacts>("Select * from hnInfo");
            await Task.Delay(10);
            var pageContact = PageInfo<Contacts>.Create(contact, page.PageNumber, page.PageSize);
            if (pageContact.Count == 0)return new ResultDTO(200, "获取失败", pageContact, ResultStatus.Fail);
            return new ResultDTO(200, "获取成功", pageContact, ResultStatus.Suceess);
        }
        public async Task<ResultDTO> Get(ContactsDTO reg)
        {
            IEnumerable<Contacts> Contact = null;
            if ((reg.Name == null || reg.Name == "") && (reg.Phone == null || reg.Phone == "") && (reg.IdCard == null || reg.IdCard == ""))
            {
                //return null;
                return new ResultDTO(200, "获取失败，条件不能全部为空", "", ResultStatus.Fail);
            }
            var sql = PetaPoco.Sql.Builder
                .Select("*")
                .From("hnInfo");
            if (reg.Id != null && reg.Id.ToString() != "" && reg.Id != Guid.Empty) sql.Where("id = @0",reg.Id);
            if (reg.Name != null && reg.Name != "") sql.Where("name=@0",reg.Name);
            if (reg.Phone != null && reg.Phone != "") sql.Where("Phone=@0", reg.Phone);
            if (reg.IdCard != null && reg.IdCard != "") sql.Where("IdCard=@0", reg.IdCard);
            Contact =  Db.Query<Contacts>(sql);
            await Task.Delay(10);
            return new ResultDTO(200, "获取成功", Contact, ResultStatus.Suceess);


        }
        public async Task<ResultDTO> GetSingle(Guid id)
        {
            var Contact = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);
            return new ResultDTO(200, "查询成功", Contact, ResultStatus.Suceess);
        }

        public async Task<ResultDTO> UpdateData(Guid id , ContactsDTO req)
        {
            var mesage = new MessageRespones();
            if (req.Name == "" && req.Phone == "" && req.IdCard == "")
            {
                return new ResultDTO(200, "没有修改后的数据。", "", ResultStatus.Fail);
            }
            if (await Db.SingleOrDefaultAsync<Contacts>("where id = @0", id) == null)
            {
                return new ResultDTO(200, "修改的数据不存在。", "", ResultStatus.Fail);
            }
            var sql = PetaPoco.Sql.Builder.Append("set ");
            if (req.Name != null && req.Name != "") sql.Append("name = @0 ", req.Name);
            if (req.IdCard != null && req.IdCard != "") sql.Append(", IdCard = @0 ", req.IdCard);
            if (req.Phone != null && req.Phone != "") sql.Append(", Phone = @0 ", req.Phone);
            sql.Append(" where id = @0", id);
            var ContactNum = await Db.UpdateAsync<Contacts>(sql);
            if (ContactNum <= 0)
            {
                return new ResultDTO(200, "更新失败。", "", ResultStatus.Fail);
            }
            var selectData = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);

            return new ResultDTO(200, "更新成功。", _mapper.Map<ContactsDTO>(selectData), ResultStatus.Suceess);

        }
    }
}
