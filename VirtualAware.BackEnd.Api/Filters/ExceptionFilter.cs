using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VirtualAware.BackEnd.Api.Models;

namespace VirtualAware.BackEnd.Api.Filters;

public class ExceptionFilter : IExceptionFilter {
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger) {
        _logger = logger;
    }

    public void OnException(ExceptionContext context) {
        _logger.LogError(context.Exception, "Exception occured when {ClientIP} {Method} {Path}{Query}",
            context.HttpContext.Connection.RemoteIpAddress, context.HttpContext.Request.Method, context.HttpContext.Request.Path, context.HttpContext.Request.QueryString);

        var result = new JsonResult(new ExceptionResponse {
            Message = context.Exception.Message,
            Exception = context.Exception.ToString()
        })
        {
            StatusCode = 500
        };

        context.Result = result;
    }
}
