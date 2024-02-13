using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using ToadaySolnBE.DTO;

namespace ToadaySolnBE.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult { StatusCode = 500, Content= JsonSerializer.Serialize(ResponseDTO<string>.Failure(context.Exception)) };
        }
    }
}
