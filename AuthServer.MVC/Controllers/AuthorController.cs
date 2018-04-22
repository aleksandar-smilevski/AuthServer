using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AuthServer.MVC.Helpers;
using AuthServer.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthServer.MVC.Controllers
{
    public class AuthorController : Controller
    {
        // GET
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri("http://localhost:5004/api/authors"));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject =
                            JsonConvert.DeserializeObject<ResponseObject<List<AuthorDto>>>(
                                await response.Content.ReadAsStringAsync());
                        return View(responseObject.Data);
                    }
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            
            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri($"http://localhost:5004/api/authors/{id}"));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonConvert.DeserializeObject<ResponseObject<AuthorDto>>(await response.Content.ReadAsStringAsync());
                        return View(responseObject.Data);
                    }
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return View();
        }
    }
}