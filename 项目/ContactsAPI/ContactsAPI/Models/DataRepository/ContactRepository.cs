using ContactsAPI.Models.config;
using ContactsAPI.Models.LoginInfo;
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
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using ContactsAPI.Models.HangfireInfo;

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

        /// <summary>
        /// 添加数据的方法
        /// </summary>
        /// <param name="reg">数据</param>
        /// <returns></returns>
        public async Task<ResultDTO> AddData(ContactsDTO reg)
        {
            var soucreContacts = _mapper.Map<Contacts>(reg);
            soucreContacts.Id = System.Guid.NewGuid();
            var selectData = await Db.SingleOrDefaultAsync<Contacts>("where phone = @0",soucreContacts.Phone);
            if (selectData != null)
            {
                return new ResultDTO(200,"该号码已存在", selectData,ResultStatus.Error);
            }
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
        /// <summary>
        /// 根据ID删除数据的方法
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取全部数据的方法
        /// </summary>
        /// <param name="page">设置 查询第几页和一页好多数据</param>
        /// <returns></returns>
        public async Task<PageInfo<Contacts>> GetData(Page page)
        {
            var contact = Db.Query<Contacts>("Select * from hnInfo");
            await Task.Delay(10);
            var pageContact = PageInfo<Contacts>.Create(contact, page.PageNumber, page.PageSize);
            //if (pageContact.Count == 0)return new ResultDTO(200, "获取失败", pageContact, ResultStatus.Fail);
            //return new ResultDTO(200, "获取成功", pageContact, ResultStatus.Suceess);
            return pageContact;
        }
        /// <summary>
        /// 根据 姓名、电话号码和身份证 查询数据
        /// </summary>
        /// <param name="reg">包含姓名、电话号码和身份证的实体类</param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        public async Task<ResultDTO> GetSingle(Guid id)
        {
            var Contact = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);
            return new ResultDTO(200, "查询成功", Contact, ResultStatus.Suceess);
        }
        /// <summary>
        /// 更新数据的方法
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="req">包含姓名、电话号码和身份证的实体类</param>
        /// <returns></returns>
        public async Task<ResultDTO> UpdateData(Guid id,ContactsDTO req)
        {
            var contactReq = _mapper.Map<Contacts>(req);
            if (contactReq.Name == "" && contactReq.Phone == "" && contactReq.IdCard == "")
            {
                return new ResultDTO(200, "没有修改后的数据。", "", ResultStatus.Fail);
            }
            if (await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id) == null)
            {
                return new ResultDTO(200, "修改的数据不存在。", "", ResultStatus.Fail);
            }
            var sql = PetaPoco.Sql.Builder.Append("set ");
            if (contactReq.Name != null && contactReq.Name != "") sql.Append("name = @0 ", contactReq.Name);
            if (contactReq.IdCard != null && contactReq.IdCard != "") sql.Append(", IdCard = @0 ", contactReq.IdCard);
            if (contactReq.Phone != null && contactReq.Phone != "") sql.Append(", Phone = @0 ", contactReq.Phone);
            sql.Append(" where id = @0", id);
            var ContactNum = await Db.UpdateAsync<Contacts>(sql);
            if (ContactNum <= 0)
            {
                return new ResultDTO(200, "更新失败。", "", ResultStatus.Fail);
            }
            var selectData = await Db.SingleOrDefaultAsync<Contacts>("where Id = @0", id);

            return new ResultDTO(200, "更新成功。", _mapper.Map<ContactsDTO>(selectData), ResultStatus.Suceess);

        }
        /// <summary>
        /// 判断是否登录成功，若成功，则返回Token。
        /// </summary>
        /// <param name="userInfo">用户登录信息的实体类</param>
        /// <returns></returns>
        public async Task<ResultDTO> UserInfo(UserInfo userInfo)
        {
            var selectUserInfo = await Db.SingleOrDefaultAsync<UserInfo>("where UserName = @0", userInfo.UserName);
            if (!(userInfo.UserName.Equals(selectUserInfo.UserName) && userInfo.Pwd.Equals(selectUserInfo.Pwd)))
            {
                return new ResultDTO(200, "账号或密码错误", "", ResultStatus.Error);
            }
            var nbf = DateTime.Now;
            var exp = nbf.AddMinutes(5);
            var myClaim = new[] {
                //new Claim(ClaimTypes.Name,selectUserInfo.UserName),

                new Claim(JwtRegisteredClaimNames.Sub,selectUserInfo.UserName),
                new Claim("Company", "hn"),
                new Claim("author", "tp"),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(exp).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(nbf).ToUnixTimeSeconds()}")
            };
            IdentityModelEventSource.ShowPII = true;

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789963258741"));

            var token = new JwtSecurityToken(
                    issuer:"tp",
                    expires:exp,
                    audience:"everyone",
                    notBefore:nbf,
                    claims:myClaim,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );
            var tokenAndTime = new {
                jwttoken = new JwtSecurityTokenHandler().WriteToken(token),
                //overdue = token.ValidTo.ToLocalTime()
                overdue = new DateTimeOffset(token.ValidTo).ToUnixTimeSeconds()
            };
            return new ResultDTO(200, "验证通过", tokenAndTime, ResultStatus.Suceess);
           
        }
        #region
        ///// <summary>
        ///// 创建定时任务
        ///// </summary>
        ///// <param name="operationHangFire"></param>
        ///// <returns></returns>
        //public async Task<ResultDTO> StartTask(OperationHangFire operationHangFire)
        //{
        //    int maxId;
        //    if (Db.ExecuteScalar<int?>("select max(id) from HangfireInfo") == null) maxId = 0;
        //    else maxId = Db.ExecuteScalar<int>("select max(id) from HangfireInfo");
        //    var requestDateTime = DateTime.Now.ToString("F");
        //    //HangfireLogger hangfireLog;
        //    //if (operationHangFire.TaskId == 0)
        //    //{

        //    //    var hangfireLog = new HangfireLogger(maxId + 1, operationHangFire.OperationCode, operationHangFire.TaskId, TaskStatusCode.Fail, requestDateTime, "");
        //    //    await Db.InsertAsync(hangfireLog);
        //    //    return new ResultDTO(400, "请求失败，任务名字不能为空", ResultStatus.Fail);
        //    //}
        //    switch (operationHangFire.OperationCode)
        //    {
        //        case 0:
        //            //await Task.Delay(10);
        //            RecurringJob.AddOrUpdate(maxId.ToString(), () => RecurrJobj(), "0 30 12 * * ?", TimeZoneInfo.Local );
        //            var hangfireLog = new HangfireLogger(maxId + 1, operationHangFire.OperationCode, maxId, TaskStatusCode.Success, requestDateTime, "12:00:00");
        //            await Db.InsertAsync(hangfireLog);
        //            return new ResultDTO(200, "请求成功，任务已开启", ResultStatus.Suceess);
        //        case 1:
        //            RecurringJob.RemoveIfExists(operationHangFire.TaskId.ToString());
        //            return new ResultDTO(200, "删除成功", ResultStatus.Suceess);
        //        default:
        //            return new ResultDTO(400, "输入错误", ResultStatus.Fail);
        //    }
        //}
        ///// <summary>
        ///// 创建队列任务
        ///// </summary>
        ///// <returns></returns>
        //public async Task<ResultDTO> FireAndForgetJobs()
        //{
        //    await Task.Delay(10);
        //    var jobId = BackgroundJob.Enqueue(()=> FireJob());
        //    if (jobId == "")
        //    {
        //        return new ResultDTO(400, "队列任务开启失败", ResultStatus.Fail);
        //    }
        //    return new ResultDTO(200, "队列任务开启成功", ResultStatus.Suceess);
        //}
        //public void FireJob()
        //{
        //    Console.WriteLine("已开启队列任务：FireJob");
        //}
        //public void RecurrJobj()
        //{
        //    Console.WriteLine("已开启定时任务：RecurrJobj");
        //}
        #endregion

    }
}
