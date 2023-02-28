using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace TransportAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                //await HandleExceptionAsync(context, ex);
            }
        }

        //private Task HandleExceptionAsync(HttpContext context, Exception ex)
        //{
        //    string result = JsonConvert.SerializeObject(new {error = ex.Message});
        //    context.Response.ContentType = "application/problem+json";
        //    if(ex is StatusException)
        //    {
        //        StatusException exception = (StatusException)ex;
        //        context.Response.StatusCode = exception.Status;
        //    }
        //    else
        //    {
        //        context.Response.StatusCode = 500;
        //    }
        //    return context.Response.WriteAsync(result);
        //}
    }
}
