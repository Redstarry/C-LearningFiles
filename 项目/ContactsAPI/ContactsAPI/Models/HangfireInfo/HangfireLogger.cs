using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.HangfireInfo
{
    [PetaPoco.TableName("HangfireInfo")]
    public class HangfireLogger
    {

        public int Id { get; set; } = 0;
        public string RequestInfo { get; set; }
        public string TaskName { get; set; }
        public TaskStatusCode TaskStatus { get; set; }
        public string RequestTime { get; set; }
        public string ExecutionTime { get; set; }
        public HangfireLogger(int Id, string RequestInfo, string TaskName, TaskStatusCode TaskStatus, string RequestTime, string ExecutionTime)
        {
            this.Id = Id;
            this.RequestInfo = RequestInfo;
            this.TaskName = TaskName;
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
