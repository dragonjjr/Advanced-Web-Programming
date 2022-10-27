using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApplication3.DBContext;
using WebApplication3.Model;

namespace WebApplication3.Handler
{
    public class BasicAuthenticationHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly testapiContext testapiContext;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,ILoggerFactory logger, UrlEncoder encoder , ISystemClock clock):base(options, logger, encoder, clock)
        {
            testapiContext = new testapiContext();
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Empty header");
            var header_key = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(header_key.Parameter);
            string credentails = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentails))
            {
                string[] array = credentails.Split(':');
                string username = array[0];
                string password = array[1];
                byte[] PasswordAsBytes = Encoding.UTF8.GetBytes(password);
                string pwd = Convert.ToBase64String(PasswordAsBytes);
                var user = testapiContext.Users.Where(x => x.Username == username && x.Password == pwd).FirstOrDefault();
                if (user == null)
                    return AuthenticateResult.Fail("User or password is not correct");

                var Claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
                var Identity = new ClaimsIdentity(Claims, Scheme.Name);
                var principal = new ClaimsPrincipal(Identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Empty header");
            }
        }
    }
}
