using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CustomMiddleware
{
    public class RequestVerifier
    {

        private readonly RequestDelegate _next;

        public DateTime Requestdate { get; set; }

        public RequestVerifier(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                System.Diagnostics.Debug.Print(context.Request.Path.ToString());
                DateTime dt = DateTime.Now;

                //Check if we are getting date in the request header from outside world
                if (!context.Request.Headers.Keys.Contains("datetime"))
                {
                    dt = Requestdate == null ? DateTime.Now : Requestdate; // Check if we are getting Request date in mock test? Assign the date to request date from Mock
                }
                else
                    dt = Convert.ToDateTime(context.Request.Headers["datetime"]); // Assign date from request header

                var checkIfItStartsWithCoffee = context.Request.Path.ToString().ToLower().StartsWith("/coffee");
                var isMethodValid = context.Request.Method.Equals("GET") || context.Request.Method.Equals("DELETE") || context.Request.Method.Equals("POST");

                if (checkIfItStartsWithCoffee && isMethodValid && (dt.Day==1 && dt.Month==4))
                {

                    await GetErrorResonse(context);

                }
                else
                {

                    await GetDifferentResonse(context); // or invoke next 
                }

            }
            catch (Exception ex)
            {
                var response = context.Response;

                var statusCode = (int)HttpStatusCode.NotImplemented;
                var message = ex.Message;
                var description = "Method not implemented";

                response.ContentType = "application/json";
                response.StatusCode = statusCode;
                await response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Description = description
                }));
            }
        }

        private async Task GetErrorResonse(HttpContext context)
        {

            var response = context.Response;

            var statusCode = 418;
            var message = "I'm a teapot";
            var description = "April fool";

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message,
                Description = description
            }));
        }

        private async Task GetDifferentResonse(HttpContext context)
        {

            var response = context.Response;

            var statusCode = (int)HttpStatusCode.OK;
            var message = "All good";
            var description = "Grab different Response";

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message,
                Description = description
            }));
        }
    }
}
