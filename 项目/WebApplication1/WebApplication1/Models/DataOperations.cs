using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Poco;

namespace WebApplication1.Models
{
    public class DataOperations
    {
        //DataRequest request = new DataRequest();
        public void IsType()
        {
            switch (DataRequest.Instance.Stat)
            {
                case "1":
                    AddData();
                    break;
                case "2":
                    UpdataData();
                    break;
                case "3":
                    break;
                case "4":
                    break;
                   
            }
        }

        public void Connec()
        { 
            
        }
        public void AddData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";

            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            
            var data = new InertData();
            data.Name = DataRequest.Instance.Name;
            data.Phone = DataRequest.Instance.PhoneNumber;
            data.IdCare = DataRequest.Instance.IDCard;
            data.Id = System.Guid.NewGuid().ToString();
            db.Insert("Info", data);
            //var upd = db.SingleOrDefault < data >()
            Respones.Instance.Result = "ok";
            Respones.Instance.Message = "添加成功";
            Respones.Instance.Stat = "新增数据";
        }

        public void UpdataData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";

            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var data = new InertData();
            foreach (var a in db.Query<InertData>("SELECT * FROM Info"))
            {
                if (DataRequest.Instance.Name == null)
                {
                    data.Name = a.Name;
                    
                }
            }
            db.Update(data);
        }


    }
}
