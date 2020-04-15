using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetaPoco;

namespace ContactsAPI.Models.HangfireInfo
{
    [PetaPoco.TableName("HangfireInfo")]
    [PetaPoco.PrimaryKey("Id",AutoIncrement =false)]
    public class HangfireLogger
    {

        /// <summary>
        /// 任务的序号
        /// </summary>
        [Column("Id")]
        public int Id { get; set; } = 0;
        /// <summary>
        /// 请求的信息
        /// </summary>
        public int RequestInfoCode { get; set; }
        /// <summary>
        /// 任务的名字
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 请求的状态， 0 成功 1 失败
        /// </summary>
        public TaskStatusCode TaskStatus { get; set; }
        /// <summary>
        /// 请求的时间
        /// </summary>
        public string RequestTime { get; set; }
        /// <summary>
        /// 执行的时间
        /// </summary>
        public string ExecutionTime { get; set; }
        public HangfireLogger(int Id, int RequestInfoCode, string TaskId,string TaskType, TaskStatusCode TaskStatus, string RequestTime, string ExecutionTime)
        {
            this.Id = Id;
            this.RequestInfoCode = RequestInfoCode;
            this.TaskId = TaskId;
            this.TaskType = TaskType;
            this.TaskStatus = TaskStatus;
            this.RequestTime = RequestTime;
            this.ExecutionTime = ExecutionTime;
        }
    }

    public enum TaskStatusCode
    {
        Success = 0,
        Fail = 1
    }
}
