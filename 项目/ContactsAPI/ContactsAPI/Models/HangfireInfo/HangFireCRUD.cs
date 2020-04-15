using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.HangfireInfo
{
    public class HangFireCRUD:IHangFireCRUD
    {
        private readonly PetaPoco.Database Db;
        private readonly int MaxId;
        private HangfireLogger hangFireLog;
        public HangFireCRUD()
        {
            Db = new PetaPoco.Database("server = .;database = ContactInformation;uid = sa; pwd = 123", "System.Data.SqlClient", null);
            if (Db.ExecuteScalar<int?>("select max(id) from HangfireInfo") == null) MaxId = 0;
            else MaxId = Db.ExecuteScalar<int>("select max(id) from HangfireInfo");
        }
        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <returns></returns>
        public async Task<ResultDTO> StartTask()
        {
            
            var requestDateTime = DateTime.Now.ToString("F");
            var NewMaxId = MaxId + 1;
            RecurringJob.AddOrUpdate(NewMaxId.ToString(), () => RecurrJobj(), "0 30 12 * * ?", TimeZoneInfo.Local);
            var hangfireLog = new HangfireLogger(NewMaxId, 0, NewMaxId.ToString(), "Recurring jobs", TaskStatusCode.Success, requestDateTime, "12:00:00");
            await Db.InsertAsync(hangfireLog);
            return new ResultDTO(200, "请求成功，任务已开启", ResultStatus.Suceess);
        }
        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="TaskId">定时任务的编号</param>
        /// <returns></returns>
        public async Task<ResultDTO> DeleRecurringJob(string TaskId)
        {
            var NewMaxId = MaxId + 1;
            RecurringJob.RemoveIfExists(TaskId);
            hangFireLog = new HangfireLogger(NewMaxId, 1, NewMaxId.ToString(), "Recurring jobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), "");
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200,"删除成功",ResultStatus.Suceess);

        }
        /// <summary>
        /// 创建队列任务
        /// </summary>
        /// <returns></returns>
        public async Task<ResultDTO> FireAndForgetJobs()
        {
            var jobId = BackgroundJob.Enqueue(() => FireJob());
            var NewMaxId = MaxId + 1;
            hangFireLog = new HangfireLogger(NewMaxId, 0, jobId, "Fire-and-forget jobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), DateTime.Now.ToString("F"));
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200, "队列任务开启成功", ResultStatus.Suceess);
        }
        /// <summary>
        /// 创建延时任务
        /// </summary>
        /// <returns></returns>
        public async Task<ResultDTO> DelayedJobs()
        {
            var NewMaxId = MaxId + 1;
            var jobId = BackgroundJob.Schedule(()=> DelayedJob(),TimeSpan.FromMinutes(10));
            hangFireLog = new HangfireLogger(NewMaxId, 0, jobId, "Delayed-jobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), DateTimeOffset.Now.AddMinutes(10).ToString("F"));
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200, "延时任务开启成功", ResultStatus.Suceess);
        }
        /// <summary>
        /// 删除延时任务
        /// </summary>
        /// <param name="jobId">延时任务的任务id</param>
        /// <returns></returns>
        public async Task<ResultDTO> DeleDelayedJobs(string jobId)
        {
            BackgroundJob.Delete(jobId);
            await Task.Delay(10);
            var NewMaxId = MaxId + 1;
            hangFireLog = new HangfireLogger(NewMaxId, 1, jobId, "Delayed-jobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), "");
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200, "删除延时任务成功", ResultStatus.Suceess);
        }
        /// <summary>
        /// 创建连续性任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<ResultDTO> ContinuationsJobs(string jobId)
        {
            var NewMaxId = MaxId + 1;
            var intJobId = Convert.ToInt32(jobId);
            BackgroundJob.ContinueJobWith(jobId, ()=> ContinuationsJob());
            var datetime = Db.ExecuteScalar<string>("select ExecutionTime from HangfireInfo where TaskId = @0", jobId);
            hangFireLog = new HangfireLogger(NewMaxId, 0, (intJobId + 1).ToString(), "Continuations-jobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), datetime);
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200, "连续性任务开启成功", ResultStatus.Suceess);
        }
        /// <summary>
        /// 删除连续性任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<ResultDTO> DeleContinuationsJobs(string jobId)
        {
            var NewMaxId = MaxId + 1;
            BackgroundJob.Delete(jobId);
            hangFireLog = new HangfireLogger(NewMaxId, 1, jobId, "ContinuationsJobs", TaskStatusCode.Success, DateTime.Now.ToString("F"), "");
            await Db.InsertAsync(hangFireLog);
            return new ResultDTO(200, "删除连续性任务成功", ResultStatus.Suceess);
        }
        public void ContinuationsJob()
        {
            Console.WriteLine("已开启延续性任务：ContinuationsJob");
        }
        public void DelayedJob()
        {
            Console.WriteLine("已开启延时任务：DelayedJob");
        }
        public void FireJob()
        {
            Console.WriteLine("已开启队列任务：FireJob");
        }
        public void RecurrJobj()
        {
            Console.WriteLine("已开启定时任务：RecurrJobj");
        }
    }
}
