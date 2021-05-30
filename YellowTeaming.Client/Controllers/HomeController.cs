using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using YellowTeaming.Client.Models;
using System.Linq;

namespace YellowTeaming.Client.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<string> Poem()
        {

            if (User.Identity.IsAuthenticated)
            {
                string accesstoken = await HttpContext.GetTokenAsync("access_token");
                string idToken = await HttpContext.GetTokenAsync("id_token");
                
                

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
                var response = await client.GetAsync("https://localhost:44350/api/poem");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return $"Status code: {response.StatusCode}";

            }

            else
            {
                return "User is not authenticated.";
            }



        }



        public async Task<string> Team()
        {

            if (User.Identity.IsAuthenticated)
            {
                string accesstoken = await HttpContext.GetTokenAsync("access_token");
                string idToken = await HttpContext.GetTokenAsync("id_token");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
                var response = await client.GetAsync("https://localhost:44350/api/team");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return $"Status code: {response.StatusCode}";

            }

            else
            {
                return "User is not authenticated.";
            }



        }


    }
}
