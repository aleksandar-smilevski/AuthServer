using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuthServer.MVC.Helpers;
using AuthServer.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthServer.MVC.Controllers
{
    [Authorize(Policy = "CanAccessPage")]
    public class AuthorController : Controller
    {
        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(token);
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
        
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(token);
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

        [HttpGet]
        [Authorize(Policy = "CanAddBooks")]
        public IActionResult Create()
        {
            var model = new AuthorDto();
            return View(model);
        }
        
        [HttpPost]
        [Authorize(Policy = "CanAddBooks")]
        public async Task<IActionResult> Create(AuthorDto newAuthor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var token = await HttpContext.GetTokenAsync("access_token");
                        client.SetBearerToken(token);
                        var jsonString = JsonConvert.SerializeObject(newAuthor);
                        
                        var response = await client.PostAsync(new Uri($"http://localhost:5004/api/authors/"), new StringContent(jsonString, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            var responseObject = JsonConvert.DeserializeObject<ResponseObject<bool>>(await response.Content.ReadAsStringAsync());
                            if(responseObject.ResponseType == ResponseType.Success)
                                return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e.Message);
                }
            }
            string messages = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            return View(messages);
        }
        
        [HttpGet]
        [Authorize(Policy = "CanAddBooks")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(token);
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
        
        [HttpPost]
        [Authorize(Policy = "CanAddBooks")]
        public async Task<IActionResult> Edit(AuthorDto newAuthor)
        {
            if (ModelState.IsValid)
            {
                if (newAuthor.Id == Guid.Empty)
                {
                    return new  BadRequestObjectResult(newAuthor);   
                }
                try
                {
                    using (var client = new HttpClient())
                    {
                        var token = await HttpContext.GetTokenAsync("access_token");
                        client.SetBearerToken(token);
                        var jsonString = JsonConvert.SerializeObject(newAuthor);
                        
                        var response = await client.PostAsync(new Uri($"http://localhost:5004/api/authors/update/"), new StringContent(jsonString, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            var responseObject = JsonConvert.DeserializeObject<ResponseObject<bool>>(await response.Content.ReadAsStringAsync());
                            if(responseObject.ResponseType == ResponseType.Success)
                                return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(e.Message);
                }
            }
            string messages = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            return View(messages);
        }
        
        [HttpPost]
        [Authorize(Policy = "CanAddBooks")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new BadRequestResult();   
            }
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(token);
                    var response = await client.PostAsync(new Uri($"http://localhost:5004/api/authors/delete/{id}"), new StringContent(""));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonConvert.DeserializeObject<ResponseObject<bool>>(await response.Content.ReadAsStringAsync());
                        if(responseObject.ResponseType == ResponseType.Success)
                            return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return RedirectToAction("Index");
        }
    }
}