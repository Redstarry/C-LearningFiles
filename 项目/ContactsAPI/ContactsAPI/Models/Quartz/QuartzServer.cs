using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using StackExchange.Redis;

namespace ContactsAPI.Models.Quartz 
{
    public class QuartzServer:IQuartzServer
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobExecutionContext _content;
        private IScheduler _scheduler;
        private ConnectionMultiplexer redis;
        private IDatabase db;
        public QuartzServer(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
            redis = ConnectionMultiplexer.Connect("192.168.175.130:6379, password=520weizhuer");
            db = redis.GetDatabase();
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
            var a = db.HashGetAll("Tgd");
     
            Console.WriteLine(a[0].Value);
            return new ResultDTO(200, "请求成功", ResultStatus.Suceess);
        }
    }
}
