using Microsoft.AspNetCore.Mvc;
using WebClient.Common;
using WebClient.Pages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using DevExtreme.AspNet.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebClient.Controllers
{
    [Route("Authen")]
    public class AuthenController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient client;
        public AuthenController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(SystemParameter.API_URL);
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ResponMessages> Login(LoginRequest VM)
        {
            ResponMessages result = new ResponMessages();
            if (VM != null)
            {
                var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(VM), Encoding.UTF8, "application/json");
                Task.Run(async () =>
                {
                    var response = await client.PostAsync($"Users/Login", httpContent);
                    var body = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponMessages>(body);
                }).Wait();
                if(result.Status == 200)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("JWToken", result.accesstoken.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("UserName", VM.UserName);
                    _httpContextAccessor.HttpContext.Session.SetString("RefreshToken", result.refreshtoken.ToString());
                }    
            }
            return result;
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ResponMessages> RefreshToken()
        {
            ResponMessages result = new ResponMessages();
            UserRefreshToken VM = new UserRefreshToken();
            VM.User = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            VM.token = _httpContextAccessor.HttpContext.Session.GetString("RefreshToken");
            var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(VM), Encoding.UTF8, "application/json");
            Task.Run(async () =>
            {
                var response = await client.PostAsync($"Users/RefreshToken", httpContent);
                var body = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponMessages>(body);
            }).Wait();
            if (result.Status == 200)
            {
                _httpContextAccessor.HttpContext.Session.SetString("JWToken", result.accesstoken.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("RefreshToken", result.refreshtoken);
            }
            return result;
        }
    }
}
