using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetaPoco;

namespace ContactsAPI.Models.Quartz
{
    [PrimaryKey("Id")]
    [PetaPoco.TableName("DemoData")]
    public class DatabaseInfo
    {
       
        public int Id { get; set; }
        public string DemoName { get; set; }
        public int DemoStatus { get; set; }
    }
}
