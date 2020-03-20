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
        //public string GuidNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IDCard { get; set; }
        public string Stat { get; set; }
    }

    public enum DataStat
    { 
        addData = 1,
        UpdateData = 2,
        selectData = 4,
        delectData = 7
    }
}
