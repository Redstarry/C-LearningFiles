using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiDemo.Models
{
    public class YzAcitonFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            YzHttpResult result = new YzHttpResult() { Result = false };
            foreach (var item in context.ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    result.Message += error.ErrorMessage + "|";
                }
            }
            context.Result = new JsonResult(result);
        }
    }
}
