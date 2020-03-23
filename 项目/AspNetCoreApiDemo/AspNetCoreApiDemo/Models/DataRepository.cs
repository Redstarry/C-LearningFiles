using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class DataRepository : IDataRepository//<RequestData>
    {
        private readonly RequestData _context;
        public string ConnecStr { get; set; }
        public PetaPoco.Database Db { get; set; }
        public DataRepository(RequestData context)
        {
            _context = context;
            ConnecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            Db = new PetaPoco.Database(ConnecStr, "System.Data.SqlClient", null);
        }
        public async Task<RequestData> AddData(RequestData req)
        {
            _context.Id = System.Guid.NewGuid();
            _context.Name = req.Name;
            _context.Phone = req.Phone;
            _context.Idcard = req.Idcard;
            await Db.InsertAsync(_context);
            return _context;
        }

        public async Task<IEnumerable<RequestData>> DeleteData(Guid id)
        {
            await Db.DeleteAsync<RequestData>("where Id = @0", id);
            var Data = Db.Query<RequestData>("select * from hnInfo");
            return Data;
        }

        public async Task<RequestData> GetRequestDatas(Guid id)
        {
            var Data = await Db.SingleOrDefaultAsync<RequestData>("where Id = @0", id);
            return Data;
        }

        public async Task<RequestData> UpdateData(RequestData req,Guid id)
        {
            var Data = await Db.SingleOrDefaultAsync<RequestData>("where Id = @0", id);
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
            return req;
        }

        public  IEnumerable<RequestData> GetRequestDatas()
        {
            var Data = Db.Query<RequestData>("select * from hnInfo");
            return Data;
        }
    }
}
