using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartZ
{
    [PersistJobDataAfterExecution] //标记job为有状态的job，多次调用job的时候，会对job进行持久化，即保存一个数据的信息。
    public class myjob : IJob
    {
        private int _count;
        public Task Execute(IJobExecutionContext context)
        {

            var name = context.JobDetail.JobType.Name;
            //var date = context.NextFireTimeUtc;
            //var Bjdate = TimeZoneInfo.ConvertTime((DateTimeOffset)date,TimeZoneInfo.Local);
            //var a = context.FireTimeUtc;
            //var Bja = TimeZoneInfo.ConvertTime(a,TimeZoneInfo.Local);
            var b = context.Trigger.Key.Name;
            var c = context.Trigger.Key.Group;
            _count = context.JobDetail.JobDataMap.GetInt("count");
            var exDate = TimeZoneInfo.ConvertTime( context.FireTimeUtc, TimeZoneInfo.Local).ToString("F");
            return Task.Run(()=> {
                ++_count;
                //Console.WriteLine(_count);
                context.JobDetail.JobDataMap.Put("count", _count);
                //Console.WriteLine(Bjdate);
                //Console.WriteLine(Bja);
                Console.WriteLine($"该任务的执行时间是：{exDate}");
            });
        }
    }
}
