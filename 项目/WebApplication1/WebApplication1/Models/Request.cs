using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DataRequest
    {
        private static DataRequest instance;
        public static DataRequest Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new DataRequest();
                }
                return instance;
            }
        }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 操作状态码， "1" : 新增数据， "2" ：更改数据, "3" ： 查询数据， "4" : 删除数据
        /// </summary>
        public string Stat { get; set; }
        /// <summary>
        /// 查询，更改数据用的查询的列名
        /// </summary>
        public string SelectField { get; set; }
        /// <summary>
        /// 查询，更改数据用的查询的列名数据
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 要删除数据的列名
        /// </summary>
        public string DeleDataField { get; set; }
        /// <summary>
        /// 要删除数据的列名的数据
        /// </summary>
        public string DeleDataValue { get; set; }

    }
    public class Selectfield
    {
        private static Selectfield instance;
        public static Selectfield Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Selectfield();
                }
                return instance;
            }
        }
        public string Field { get; set; }
        public string FieldValue { get; set; }
    }
}

