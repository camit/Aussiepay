using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomMiddleware
{
    public class RequestTime
    {
        private readonly RequestDelegate _next;

        public RequestTime(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var httpRequestTimeFeature = new HttpRequestTimeFeature();
            context.Features.Set<IHttpRequestTimeFeature>(httpRequestTimeFeature);

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }

    public class HttpRequestTimeFeature : IHttpRequestTimeFeature
    {
        public DateTime RequestTime { get; }

        public HttpRequestTimeFeature()
        {
            RequestTime = DateTime.Now;
        }
    }
    public interface IHttpRequestTimeFeature
    {
        DateTime RequestTime { get; }
    }
}
