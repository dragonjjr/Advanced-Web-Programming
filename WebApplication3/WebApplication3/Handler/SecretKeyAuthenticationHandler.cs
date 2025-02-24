﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApplication3.DBContext;
using Serilog.Parsing;

namespace WebApplication3.Handler
{
    public class SecretKeyAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly testapiContext testapiContext;
        public SecretKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            testapiContext = new testapiContext();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Token"))
                return AuthenticateResult.Fail("Empty header");
            var header_token = Request.Headers["Token"];
            var header_time = Request.Headers["Time"];
            
            if (!string.IsNullOrEmpty(header_token) && !string.IsNullOrEmpty(header_time))
            {
                var token = Helperscs.GetToken(header_time);
                if (header_token == token)
                {
                    var Claims = new[] { new Claim(ClaimTypes.Name, "abc") };
                    var Identity = new ClaimsIdentity(Claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(Identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Token invalid");
                }
            }
            else
            {
                return AuthenticateResult.Fail("Empty header");
            }
        }
    }
}
