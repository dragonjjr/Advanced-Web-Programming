using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using WebClient.Common;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WebClient.Controllers
{
    [Route("Actor")]
    public class ActorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient client;
        //private string tokentring = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImp0aSI6IjUxMjU4YTNmLTYzMTktNGFlNC1hNzQ2LWJlYzllODVkNmRmOCIsImV4cCI6MTY2Njg0NjY5MiwiaXNzIjoiTmV0QVBJIiwiYXVkIjoiQ2xpZW50In0.C6geTz3f6qm8Xx2XmHH9vGNT2QuLxr56UOGI797dpus";
        public ActorController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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
        [HttpPost("GetActor")]
        public async Task<ResponMessages> GetActor()
        {
            ResponMessages result = new ResponMessages();
            Task.Run(async () =>
            {
                var response = await client.GetAsync($"ActorControllercs/GetAll");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponMessages>(body);
                    var m = JsonConvert.DeserializeObject<List<Actor>>(result.Data.ToString());
                    result.Data = m;
                }
                else
                {
                    result.Status = 0;
                    result.messages = response.StatusCode.ToString();
                }
            }).Wait();
            return result;
        }
    }
}
