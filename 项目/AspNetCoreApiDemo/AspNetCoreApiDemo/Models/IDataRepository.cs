using AspNetCoreApiDemo.Data;
using AspNetCoreApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public interface IDataRepository//<RequestData> where RequestData:class
    {
        //IEnumerable<RequestData> GetRequestDatas();
        //IEnumerable<RequestData> GetRequestDatas(PageProp parameters);
        Page<ResponesData> GetRequestDatas();
        //Task<RequestData> GetRequestDatas();
        Task<ResponesData> GetRequestDatas(Guid id);
        Task<ResponesData> AddData(RequestData req);
        //Task<ActionResult<IEnumerable<ResponesData>>> DeleteData(Guid id);
        Task<ActionResult<string>> DeleteData(Guid id);
        Task<ResponesData> UpdateData(RequestData req,Guid id);
    }
}
