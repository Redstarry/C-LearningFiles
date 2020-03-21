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
                    SelectData();
                    break;
                case "4":
                    DeleData();
                    break;
                   
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public void AddData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";

            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            
            var data = new InertData();
            data.Name = DataRequest.Instance.Name;
            data.Phone = DataRequest.Instance.PhoneNumber;
            data.IdCard = DataRequest.Instance.IdCard;
            data.Id = System.Guid.NewGuid().ToString();
            db.Insert("hnInfo", data);
            //var upd = db.SingleOrDefault < data >()
            Respones.Instance.Result = "ok";
            Respones.Instance.Message = "添加成功";
            Respones.Instance.Stat = "新增数据";
        }
        /// <summary>
        /// 更新数据
        /// </summary>

        public void UpdataData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";

            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var data = new InertData();//实例化 数据库表的实体
            var SelectData = db.SingleOrDefault<InertData>($"SELECT * FROM hnInfo where Phone = '{DataRequest.Instance.FieldValue}'");//获取数据库中的数据。
            if (DataRequest.Instance.Name == null) data.Name = SelectData.Name;//判断 入参的数据是否为null
            else data.Name = DataRequest.Instance.Name;

            if (DataRequest.Instance.PhoneNumber == null || DataRequest.Instance.PhoneNumber == "") data.Phone = DataRequest.Instance.FieldValue;
            else data.Phone = DataRequest.Instance.PhoneNumber;

            if (DataRequest.Instance.IdCard == null || DataRequest.Instance.IdCard == "") data.IdCard = SelectData.IdCard;
            else data.IdCard = DataRequest.Instance.IdCard;

            if(SelectData.Id == null || SelectData.Id == "")data.Id = System.Guid.NewGuid().ToString();
            data.Id = SelectData.Id;
            int Result = db.Update(data);
            if (Result > 0)
            {
                Respones.Instance.Result = "ok";
                Respones.Instance.Message = "更新成功";
                Respones.Instance.Stat = "更新数据";
            }
            
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        public void SelectData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";

            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var data = new InertData();
            //IEnumerable<InertData> SelectData;

            if (DataRequest.Instance.SelectField == null || DataRequest.Instance.SelectField == "")
            {
                Respones.Instance.SelectData = db.Query<InertData>($"SELECT * FROM hnInfo");//获取数据库中的数据。
            }
            else
            {
                Respones.Instance.SelectData = db.Query<InertData>($"SELECT * FROM hnInfo where {DataRequest.Instance.SelectField} = '{DataRequest.Instance.FieldValue}'");//获取数据库中的数据。
            }
            Respones.Instance.Result = "OK";
            Respones.Instance.Message = "查询成功";
            Respones.Instance.Stat = "查询数据"; 
            
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleData()
        {
            string connecStr = "server = .;database = ContactInformation; uid = sa; pwd = 123";
            var db = new PetaPoco.Database(connecStr, "System.Data.SqlClient", null);
            var data = new InertData();
            if (DataRequest.Instance.DeleDataField != null || DataRequest.Instance.DeleDataField != "")
            {
                int Result = db.Delete<InertData>($"Delete from hnInfo where {DataRequest.Instance.DeleDataField} = '{DataRequest.Instance.DeleDataValue}'");
                if (Result > 0)
                {
                    Respones.Instance.Result = "OK";
                    Respones.Instance.Message = "删除成功";
                    Respones.Instance.Stat = "删除数据";
                }
            }
            else
            {
                Console.WriteLine("删除的条件不能为空。");
            }
            
            
        }


    }
}
