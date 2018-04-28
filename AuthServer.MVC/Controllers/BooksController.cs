using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuthServer.MVC.Helpers;
using AuthServer.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthServer.MVC.Controllers
{
    [Authorize(Policy = "CanAccessPage")]
    public class BooksController : Controller
    {
        // GET
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri("http://localhost:5004/api/books/all"));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject =
                            JsonConvert.DeserializeObject<ResponseObject<List<BookDto>>>(
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
                    var response = await client.GetAsync(new Uri($"http://localhost:5004/api/books/{id}"));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonConvert.DeserializeObject<ResponseObject<BookDto>>(await response.Content.ReadAsStringAsync());
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
        
        [Authorize(Policy = "CanAddBooks")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new BookDto();
            return View(model);
        }
        
        [Authorize(Policy = "CanAddBooks")]
        [HttpPost]
        public async Task<IActionResult> Create(BookDto newBook)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var jsonString = JsonConvert.SerializeObject(newBook);
                        
                        var response = await client.PostAsync(new Uri($"http://localhost:5004/api/books/"), new StringContent(jsonString, Encoding.UTF8, "application/json"));
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
        
        [Authorize(Policy = "CanAddBooks")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri($"http://localhost:5004/api/books/{id}"));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseObject = JsonConvert.DeserializeObject<ResponseObject<BookDto>>(await response.Content.ReadAsStringAsync());
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
        
        [Authorize(Policy = "CanAddBooks")]
        [HttpPost]
        public async Task<IActionResult> Edit(BookDto newBook)
        {
            if (ModelState.IsValid)
            {
                if (newBook.Id == Guid.Empty)
                {
                    return new  BadRequestObjectResult(newBook);   
                }
                try
                {
                    using (var client = new HttpClient())
                    {
                        var jsonString = JsonConvert.SerializeObject(newBook);
                        
                        var response = await client.PostAsync(new Uri($"http://localhost:5004/api/books/update/"), new StringContent(jsonString, Encoding.UTF8, "application/json"));
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

        [Authorize(Policy = "CanAddBooks")]
        [HttpPost]
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
                    var response = await client.PostAsync(new Uri($"http://localhost:5004/api/books/delete/{id}"), new StringContent(""));
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