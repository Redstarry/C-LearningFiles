using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Poco
{
    [PetaPoco.TableName("hnInfo")]
    //[PetaPoco.PrimaryKey("Phone")]
    public class InertData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
    }
}
