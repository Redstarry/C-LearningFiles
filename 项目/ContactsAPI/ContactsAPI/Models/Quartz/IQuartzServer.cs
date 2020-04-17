using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models.Quartz
{
    public interface IQuartzServer
    {
        Task<ResultDTO> TimedTasks(RequestDuartzInfo requestDuartzInfo);
    }
}
