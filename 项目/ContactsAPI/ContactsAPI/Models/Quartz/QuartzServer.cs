using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace ContactsAPI.Models.Quartz 
{
    public class QuartzServer:IQuartzServer
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobExecutionContext _content;
        private IScheduler _scheduler;
        public QuartzServer(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }
        public async Task<ResultDTO> TimedTasks(RequestDuartzInfo requestDuartzInfo)
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Start();
            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(requestDuartzInfo.ExDate, x=>x.InTimeZone(TimeZoneInfo.Local)).Build();
            var jobDetail = JobBuilder.Create<myJob>()
                .UsingJobData("id",requestDuartzInfo.id)
                .WithIdentity(requestDuartzInfo.TaskName, requestDuartzInfo.GroupName).Build();
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return new ResultDTO(200, "请求成功", ResultStatus.Suceess);
        }
    }
}
