using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using PetaPoco;
using ContactsAPI.Models.Quartz;

namespace ContactsAPI.Models.Quartz
{
    public class myJob : IJob
    {
        private readonly PetaPoco.Database Db;
        public myJob()
        {
            Db = new Database("server=.;database=ContactInformation;uid=sa;pwd=123","System.Data.SqlClient",null);
        }
        public Task Execute(IJobExecutionContext context)
        {
            var id = context.JobDetail.JobDataMap.GetString("id");
            var name = context.JobDetail.Key.Name;
            var group = context.JobDetail.Key.Group;
            var startDateUTC = context.Trigger.StartTimeUtc;
            var startDateBJ = TimeZoneInfo.ConvertTime(startDateUTC, TimeZoneInfo.Local);
            var endDateUTC = context.Trigger.EndTimeUtc;
            var endDateBJ = TimeZoneInfo.ConvertTime((DateTimeOffset)endDateUTC, TimeZoneInfo.Local);
            var exDateUTC = context.FireTimeUtc;
            var exDateBJ = TimeZoneInfo.ConvertTime(exDateUTC, TimeZoneInfo.Local);
            var nextDateUTC = context.NextFireTimeUtc;
            var nextDateBJ = TimeZoneInfo.ConvertTime((DateTimeOffset)nextDateUTC, TimeZoneInfo.Local);
            var prevDateUTC = context.PreviousFireTimeUtc;
            var PrevDateBJ = TimeZoneInfo.ConvertTime((DateTimeOffset)prevDateUTC, TimeZoneInfo.Local);
            return Task.Run(()=> {
                demo a = new demo();
                var b = a.Demo(id).Result;
                Console.WriteLine($"Id：{id}, 任务的名称：{name}，任务的组名：{group}，IsComplted : {b}, 开始时间：{startDateBJ}，结束时间：{endDateBJ}, 当前执行时间：{exDateBJ}，下次执行时间：{nextDateBJ}，上次执行时间：{PrevDateBJ}");
            });
        }
    }
}
public class demo
{
    private readonly PetaPoco.Database Db;
    public demo()
    {
        Db = new Database("server=.;database=ContactInformation;uid=sa;pwd=123", "System.Data.SqlClient", null);
    }
    public async Task<bool> Demo(string id)
    {
        var data = Db.SingleOrDefault<DatabaseInfo>($"select DemoStatus from DemoData where id = {id}");
        await Task.Delay(10);
        if (data.DemoStatus == 0) return true;
        else return false;
    }
}
