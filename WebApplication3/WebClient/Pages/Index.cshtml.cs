using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Pages
{
    public class IndexModel : PageModel
    {
        public IHttpClientFactory _httpClientFactory;
        public IHttpContextAccessor _httpContextAccessor;
        public HttpClient client;
        public IndexModel(IHttpClientFactory IHttpClientFactory, IHttpContextAccessor IHttpContextAccessor, HttpClient HttpClient)
        {
            _httpClientFactory = IHttpClientFactory;
            _httpContextAccessor = IHttpContextAccessor;
            client = HttpClient;
        }
        public ActionResult OnGet()
        {
            return Page();
        }
    }
}
