using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public interface IDataRepository//<RequestData> where RequestData:class
    {
        IEnumerable<RequestData> GetRequestDatas();

        //Task<RequestData> GetRequestDatas();
        Task<RequestData> GetRequestDatas(Guid id);
        Task<RequestData> AddData(RequestData req);
        Task<IEnumerable<RequestData>> DeleteData(Guid id);
        Task<RequestData> UpdateData(RequestData req,Guid id);
    }
}
