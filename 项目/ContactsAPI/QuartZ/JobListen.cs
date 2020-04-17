using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace QuartZ
{
    class JobListen : IJobListener
    {
        public string Name => GetType().Name;

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            string name = context.JobDetail.Key.Name;
            return Task.Run(()=> {
                Console.WriteLine($"Name：{name} 的任务被拒绝执行");
            });
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            string name = context.JobDetail.Key.Name;
            return Task.Run(()=> {
                Console.WriteLine($"Name：{name} 的任务将要被执行。");
            });
            
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            string name = context.JobDetail.Key.Name;
            return Task.Run(()=> {
                Console.WriteLine($"Name：{name} 的任务已经执行。");
            });
        }
    }
}
