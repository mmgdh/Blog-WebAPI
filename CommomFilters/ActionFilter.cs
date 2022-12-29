using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Web.Http.Results;

namespace CommomFilters;

public class ActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objRst)
        {
            if (objRst.Value is ApiResult)
                return;
            context.Result = new ObjectResult(new ApiResult
            {
                Code = 200,
                Message = string.Empty,
                Data = objRst.Value
            });
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
