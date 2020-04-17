using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace QuartZ
{
    class Program
    {
        static void Main(string[] args)
        {
            ISchedulerFactory scheduler = new StdSchedulerFactory();

            var scheduler1 = scheduler.GetScheduler().Result;
            var SetDate = DateTime.Now.AddMinutes(2);
            var jobDetail = JobBuilder.Create<myjob>()
                .WithIdentity("job1","group1")
                .UsingJobData("count", 1)
                .Build();

            //var trigger = TriggerBuilder.Create().WithSimpleSchedule(x=>x.WithIntervalInSeconds(5).RepeatForever()).Build();\

            var trigger = TriggerBuilder.Create()
                .WithIdentity("1a", "2a")
                //.StartAt(SetDate)  //设置指定时间执行
                //.WithSimpleSchedule(x=>x.WithIntervalInSeconds(5).WithRepeatCount(2)) //设置每隔5秒执行，重复2次
                //.EndAt() //设置结束时间
                .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(5).WithRepeatCount(3)) // 设置永远每5秒重复执行，但是重复2次
                 .Build();

            scheduler1.ScheduleJob(jobDetail, trigger);
            scheduler1.ListenerManager.AddJobListener( new JobListen() );
            scheduler1.Start();
            Console.ReadKey();
        }
    }
}
