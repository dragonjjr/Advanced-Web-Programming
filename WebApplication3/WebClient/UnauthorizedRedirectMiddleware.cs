using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;

namespace WebClient
{
    public class UnauthorizedRedirectMiddleware
    {
        private const string authenticationPagePath = "/Login";
        private const string forbidden = "/Error";
        private readonly RequestDelegate _next;
        public UnauthorizedRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if ((context.Session.GetString("JWToken") != null && context.Session.GetString("UserInfo") != null && context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated
                || IsAllowAnonymous(context)))
            {
                await _next(context);
            }
            else
            {
                context.Response.Redirect(authenticationPagePath);
            }
        }
        
        private static bool IsAllowAnonymous(HttpContext context)
        {
            string referer = context.Request.Headers["Referer"];
            return context.Request.Path.HasValue && context.Request.Path.StartsWithSegments(authenticationPagePath)
                || referer != null && referer.Contains(authenticationPagePath);
        }
    }
}
