using System.Text;
using System;
using WebApplication3.DBContext;
using System.Security.Cryptography;
using System.Linq;
using WebApplication3.Model;
using Org.BouncyCastle.Utilities.Encoders;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using static WebApplication3.Model.General;

namespace WebApplication3.Repository
{
    public class UserRepository
    {
        private testapiContext testapiContext;
        public UserRepository()
        {
            testapiContext = new testapiContext();
        }
        public string Authenticate(LoginModel VM)
        {
            if (!string.IsNullOrEmpty(VM.username) && !string.IsNullOrEmpty(VM.password))
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] PasswordAsBytes = Encoding.UTF8.GetBytes(VM.password);
                    string pwd = Convert.ToBase64String(PasswordAsBytes);
                    User user = testapiContext.Users.Where(x => x.Username == VM.username && x.Password == pwd).FirstOrDefault();
                    string authenString = user.Username + ":" + user.Password;
                    return Helperscs.Base64Encode(authenString);
                }
            }
            return null;
        }

        public ResponMessages Login(LoginModel VM)
        {
            ResponMessages rs = new ResponMessages();
            if (!string.IsNullOrEmpty(VM.username) && !string.IsNullOrEmpty(VM.password))
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] PasswordAsBytes = Encoding.UTF8.GetBytes(VM.password);
                    string pwd = Convert.ToBase64String(PasswordAsBytes);
                    User user = testapiContext.Users.Where(x => x.Username == VM.username && x.Password == pwd).FirstOrDefault();
         
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var token = Helperscs.CreateToken(authClaims);
                    var refreshToken = Helperscs.GenerateRefreshToken();
                    user.Refreshtoken = refreshToken;
                    user.Refreshtokenexpirytime = DateTime.Now.AddDays(Helperscs.JWT_Time);
                    rs.accesstoken = new JwtSecurityTokenHandler().WriteToken(token);
                    rs.refreshtoken = refreshToken;
                    rs.Status = 200;
                    testapiContext.Users.Update(user);
                    testapiContext.SaveChanges();
                }
            }
            return rs;
        }

        public ResponMessages RefreshToken(UserRefreshToken user)
        {
            ResponMessages rs = new ResponMessages();
            if (user != null)
            {
                var new_user = testapiContext.Users.Where(x=>x.Username == user.User && x.Refreshtoken == user.token).FirstOrDefault();//&& x.Refreshtokenexpirytime <= DateTime.Now
                if (new_user != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.User),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var token = Helperscs.CreateToken(authClaims);
                    var refreshToken = Helperscs.GenerateRefreshToken();
                    new_user.Refreshtoken = refreshToken;
                    new_user.Refreshtokenexpirytime = DateTime.Now.AddDays(Helperscs.JWT_Time);
                    rs.accesstoken = new JwtSecurityTokenHandler().WriteToken(token);
                    rs.refreshtoken = refreshToken;
                    testapiContext.Users.Update(new_user);
                    testapiContext.SaveChanges();
                }
                else
                {
                    rs.messages = "Refresh token invalid!";
                }
                
            }
            return rs;
        }
    }
}
