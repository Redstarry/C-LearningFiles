using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.HangfireInfo
{
    public interface IHangFireCRUD
    {
        Task<ResultDTO> StartTask();
        Task<ResultDTO> FireAndForgetJobs();
        Task<ResultDTO> DelayedJobs();
        Task<ResultDTO> ContinuationsJobs(string jobId);
        Task<ResultDTO> DeleContinuationsJobs(string jobId);

        Task<ResultDTO> DeleDelayedJobs(string jobId);
        Task<ResultDTO> DeleRecurringJob(string jobId);
    }
}
