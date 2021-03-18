using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace net5Middleware
{
    public class justValidator
    {
        public RequestDelegate _next { get; }

        public justValidator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            StringValues authorizationToken;

            context.Request.Headers.TryGetValue("x-id", out authorizationToken);

            if (authorizationToken.Count > 0 && authorizationToken[0] == "confusing21")
                await _next(context);
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Invalid header token");

            return;
        }
    }
}
