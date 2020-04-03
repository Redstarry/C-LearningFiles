using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class ResultDTO
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public ResultDTO(int? code = null, string message = null, object result = null, ResultStatus resultStatus = ResultStatus.Suceess)
        {
            Code = code;
            Message = message;
            Result = result;
            ResultStatus = resultStatus;
        }
    }
    public enum ResultStatus
    {
        Suceess = 1,
        Fail = 0,
        ConfirmIsContinue = 2,
        Error = 3
    }
}
