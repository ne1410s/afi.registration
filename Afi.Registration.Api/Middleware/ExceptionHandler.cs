using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Afi.Registration.Api.Models;
using Afi.Registration.Domain.Errors;
using Microsoft.AspNetCore.Http;

namespace Afi.Registration.Api.Middleware
{
    /// <summary>
    /// Exception handling middleware.
    /// </summary>
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class.
        /// </summary>
        /// <param name="next">The request processing.</param>
        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <returns>An asynchronous task.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                HttpStatusCode responseCode;
                HttpErrorResponse response;
                var errorType = ex.GetType().Name;
                if (ex is ValidationException valEx)
                {
                    responseCode = HttpStatusCode.UnprocessableEntity;
                    response = new(errorType, valEx.Message, valEx.Errors);
                }
                else if (ex is PersistenceException dataEx)
                {
                    responseCode = HttpStatusCode.BadRequest;
                    response = new(errorType, dataEx.Message);
                }
                else
                {
                    responseCode = HttpStatusCode.InternalServerError;
                    response = new(errorType, ex.Message);
                }

                httpContext.Response.StatusCode = (int)responseCode;
                httpContext.Response.ContentType = "application/json";
                var responseJson = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(responseJson);
            }
        }
    }
}
