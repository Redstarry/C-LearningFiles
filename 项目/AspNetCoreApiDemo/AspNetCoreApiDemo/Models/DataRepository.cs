using AspNetCoreApiDemo.Data;
using AspNetCoreApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class DataRepository : IDataRepository//<RequestData>
    {
        private readonly ResponesData _context;
        public string ConnecStr { get; set; }
        public PetaPoco.Database Db { get; set; }
        public DataRepository(ResponesData context)
        {
            _context = context;
            ConnecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            Db = new PetaPoco.Database(ConnecStr, "System.Data.SqlClient", null);
        }
        public async Task<ResponesData> AddData(RequestData req)
        {
            _context.Id = System.Guid.NewGuid();
            _context.Name = req.Name;
            _context.Phone = req.Phone;
            _context.Idcard = req.Idcard;
            await Db.InsertAsync(_context);
            return _context;
        }

        public async Task<ActionResult<IEnumerable<ResponesData>>> DeleteData(Guid id)
        {
            await Db.DeleteAsync<ResponesData>("where Id = @0", id);
            var Data = Db.Query<ResponesData>("select * from hnInfo");
            return Data.ToList();
        }

        public async Task<ResponesData> GetRequestDatas(Guid id)
        {
            var Data = await Db.SingleOrDefaultAsync<ResponesData>("where Id = @0", id);
            return Data;
        }

        public async Task<ResponesData> UpdateData(RequestData req,Guid id)
        {
            var Data = await Db.SingleOrDefaultAsync<ResponesData>("where Id = @0", id);
            //var requestData = new RequestData();
            if (req.Name == null || req.Name == "") _context.Name = Data.Name;
            else _context.Name = req.Name;
            if (req.Phone == null || req.Phone == "") _context.Phone = Data.Phone;
            else _context.Phone = req.Phone;
            if (req.Idcard == null || req.Idcard == "") _context.Idcard = Data.Idcard;
            else _context.Idcard = req.Idcard;
            _context.Id = Data.Id;
            int Result = await Db.UpdateAsync(_context);
            if(Result > 0) return _context;
            return _context;
        }

        //public IEnumerable<RequestData> GetRequestDatas(PageProp parameters)
        //{
        //    //var aqueryData = _context as IQueryable<RequestData>;
        //    var Data = Db.Query<RequestData>("select * from hnInfo");
        //    Data = Data.Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);
        //    return Data;
        //}
        public Page<ResponesData> GetRequestDatas()
        {
            var result = Db.Page<ResponesData>(1, 5, "select * from hnInfo");
            return result;
        }

    }
}
