using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.Quartz
{
    public class RequestDuartzInfo
    {
        /// <summary>
        /// 任务的名字
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务组的名字
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 任务执行的时间，使用cron格式
        /// </summary>
        public string ExDate { get; set; }
        /// <summary>
        /// 操作任务的ID
        /// </summary>
        public string id { get; set; }
    }
}
