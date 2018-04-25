﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuthServer.MVC.Helpers;
using AuthServer.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AuthServer.MVC.Controllers
{
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
        public async Task<IActionResult> Create()
        {
            var model = new AuthorDto();
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto newAuthor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
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
    }
}