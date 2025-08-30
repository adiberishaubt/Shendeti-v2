using System.Net;
using System.Text;
using Shendeti.Infrastructure.Exceptions;

namespace Shendeti.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EntityNullException e)
        {
            context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(e.Message));
        }
        catch (ValidationException e)
        {
            context.Response.StatusCode = (int) HttpStatusCode.UnprocessableContent;
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(e.Message));
        } 
    }
}