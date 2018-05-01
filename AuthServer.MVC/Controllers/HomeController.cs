using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthServer.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuthServer.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CallAllAuthorsApi()
        {
            var access_token = await HttpContext.GetTokenAsync("access_token");

            using (var client = new HttpClient())
            {
                client.SetBearerToken(access_token);
                var content = await client.GetStringAsync( new Uri("http://localhost:5004/api/authors"));
                Console.WriteLine(JObject.Parse(content));
                return null;
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("oidc");
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index");
        }
    }
}
