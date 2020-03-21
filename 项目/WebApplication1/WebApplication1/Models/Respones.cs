using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Poco;

namespace WebApplication1.Models
{
    public class Respones
    {
        private static Respones instance;
        public static Respones Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Respones();
                }
                return instance;
            }
        }

        public string Result { get; set; }
        public string Message { get; set; }
        public string Stat { get; set; }


        public IEnumerable<InertData> SelectData;
    }
}
