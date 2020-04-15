using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.HangfireInfo
{
    public class OperationHangFire
    {
        /// <summary>
        /// 操作任务的操作码：0 新建；1. 删除
        /// </summary>
        public int OperationCode { get; set; }
        /// <summary>
        /// 要操作任务的编号
        /// </summary>
        public int TaskId { get; set; }
    }
}
