using BeajLearner.Application.Exceptions;
using BeajLearner.Application.Wrappers;
using Serilog;
using System.Net;
using System.Text.Json;

namespace BeajLearner.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Log.Information(context.Request.ToString());

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                Log.Error($"Message: {error.Message}, StackTrace: {error.StackTrace} ");

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { succeeded = false, message = error?.Message };

                switch (error)
                {
                    case Application.Exceptions.ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
