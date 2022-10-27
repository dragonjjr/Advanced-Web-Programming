using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace WebClient.Pages
{
    public class HomeModel : PageModel
    {
        public IHttpClientFactory _httpClientFactory;
        public IHttpContextAccessor _httpContextAccessor;
        public HttpClient client;
        public HomeModel(IHttpClientFactory IHttpClientFactory, IHttpContextAccessor IHttpContextAccessor, HttpClient HttpClient)
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
