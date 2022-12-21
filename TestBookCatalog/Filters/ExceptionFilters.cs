using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TestBookCatalog.Filters
{
    public class ExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ArgumentException error:
                    context.Result = new NotFoundObjectResult(error.Message);
                    break;
                case InvalidOperationException error:
                    context.Result = new BadRequestObjectResult(error.Message);
                    break;
            }
        }
    }
}
