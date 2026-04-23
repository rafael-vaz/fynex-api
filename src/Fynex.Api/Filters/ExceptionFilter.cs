using Fynex.Communication.Responses;
using Fynex.Exception;
using Fynex.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fynex.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {

        if (context.Exception is FynexException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }

    }

    private void HandleProjectException(ExceptionContext context)
    {
        var fynexException = (FynexException)context.Exception;
        var errorResponse = new ResponseErrorJson(fynexException.GetErrors());

        context.HttpContext.Response.StatusCode = fynexException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
