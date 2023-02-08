using Newtonsoft.Json;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Cliente.Application.Common.Exceptions;

namespace Cliente.WebAPI.App.Middleware
{
    public class ErrorDetails
    {
        public string? ErrorMessage { get; set; }
        public string? ErrorType { get; set; }
    }
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate RequestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private const int DefaultStatusCode = (int)HttpStatusCode.BadRequest;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.RequestDelegate = requestDelegate;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await RequestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string statusCode;
            string result;

            _logger.LogError(exception.ToString());

            switch (exception)
            {
                case DbUpdateException:
                    result = JsonConvert.SerializeObject(new ErrorDetails
                    {
                        ErrorMessage = exception.InnerException?.Message ?? exception.Message,
                        ErrorType = exception.GetType().Name
                    });
                    statusCode = $"{(int)HttpStatusCode.InternalServerError}";
                    break;
                case ValidationException validationException:
                    result = GetCustomJsonResult(validationException.Errors);
                    statusCode = GetCustomStatusCode(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    result = JsonConvert.SerializeObject(notFoundException.Errors);
                    statusCode = $"{(int)HttpStatusCode.NotFound}";
                    break;
                default:
                    result = JsonConvert.SerializeObject(new List<string> { exception.Message });
                    statusCode = $"{(int)HttpStatusCode.InternalServerError}";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ParseToHttpStatusCode(statusCode);

            return context.Response.WriteAsync(result);
        }

        private static int ParseToHttpStatusCode(string codeString)
        {
            try
            {
                return int.Parse(codeString);
            }
            catch (Exception)
            {
                return DefaultStatusCode;
            }
        }

        private static string GetCustomStatusCode(IReadOnlyList<(string, string)> validationErrors)
        {
            var defaultStatusCode = DefaultStatusCode.ToString();

            for (var i = 0; i < validationErrors.Count; i++)
            {
                if (validationErrors[i].Item2 == defaultStatusCode) continue;

                defaultStatusCode = validationErrors[i].Item2;
                break;
            }

            return defaultStatusCode;
        }

        private static string GetCustomJsonResult(IEnumerable<(string, string)> validationErrors)
        {
            return JsonConvert.SerializeObject(validationErrors.Select(x => x.Item1));
        }

    }
}