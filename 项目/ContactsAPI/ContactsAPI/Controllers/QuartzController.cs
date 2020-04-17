using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsAPI.Models.Quartz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuartzController : ControllerBase
    {
        private readonly IQuartzServer _quartzServer;

        public QuartzController(IQuartzServer quartzServer)
        {
            _quartzServer = quartzServer??throw new ArgumentNullException(nameof(quartzServer));
        }
        [HttpPost]
        public async Task<IActionResult> Get(RequestDuartzInfo requestDuartzInfo)
        {
            return Ok(await _quartzServer.TimedTasks(requestDuartzInfo));
        }
    }
}