using ClinicPro.Core.Common;
using Microsoft.AspNetCore.Http;

namespace ClinicPro.Infrastructure.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Dictionary<string, (int count, DateTime resetTime)> _requestCounts = new();

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var path = context.Request.Path.ToString();

            if (path.StartsWith("/api/v1/role")){

                var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                if(!_requestCounts.ContainsKey(key) || _requestCounts[key].resetTime < DateTime.Now)
                {
                    _requestCounts[key] = (0, DateTime.Now.AddMinutes(1));
                }

                if (_requestCounts[key].count >= 3)
                {
                    //context.Response.StatusCode = 429;
                    //await context.Response.WriteAsync("Too many requests. Try again later.");
                    //return;
                    throw new RateLimitExceededException("Se supero el limite de solicitudes permitidas para la api Role.");
                }

                _requestCounts[key] = (_requestCounts[key].count + 1, _requestCounts[key].resetTime);

            }

            await _next(context);

        }




    }
}
