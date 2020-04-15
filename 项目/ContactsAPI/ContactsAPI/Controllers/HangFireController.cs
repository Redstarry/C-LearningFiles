using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models.HangfireInfo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [EnableCors("Domain")]
    [Route("v1/HangFire")]
    [ApiController]
    public class HangFireController : ControllerBase
    {
        private readonly IHangFireCRUD hangFireCRUD;

        public HangFireController(IHangFireCRUD _hangFireCRUD)
        {
            hangFireCRUD = _hangFireCRUD??throw new ArgumentNullException(nameof(_hangFireCRUD));
        }
        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("TimedTasks")]
        public async Task<IActionResult> TimedTasks()
        {
            return Ok(await hangFireCRUD.StartTask());
        }
        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="TaskId">定时任务编号</param>
        /// <returns></returns>
        [HttpDelete("DeleRecurringJob")]
        public async Task<IActionResult> DeleRecurringJob(string TaskId)
        {
            return Ok(await hangFireCRUD.DeleRecurringJob(TaskId));
        }
        /// <summary>
        /// 创建队列任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("FireAndForgetJobs")]
        public async Task<IActionResult> FireAndForgetJobs()
        {
            return Ok(await hangFireCRUD.FireAndForgetJobs());
        }
        /// <summary>
        /// 创建延时任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("DelayedJobs")]
        public async Task<IActionResult> DelayedJobs()
        {
            return Ok(await hangFireCRUD.DelayedJobs());
        }
        /// <summary>
        /// 删除延时任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpDelete("DeleDelayedJobs")]
        public async Task<IActionResult> DeleDelayedJobs(string jobId)
        {
            return Ok(await hangFireCRUD.DeleDelayedJobs(jobId));
        }
        /// <summary>
        /// 创建连续性任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpPost("ContinuationsJobs")]
        public async Task<IActionResult> ContinuationsJobs(string jobId)
        {
            return Ok(await hangFireCRUD.ContinuationsJobs(jobId));
        }
        /// <summary>
        /// 删除连续性任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpDelete("DeleContinuationsJobs")]
        public async Task<IActionResult> DeleContinuationsJobs(string jobId)
        {
            return Ok(await hangFireCRUD.DeleContinuationsJobs(jobId));
        }
    }
}