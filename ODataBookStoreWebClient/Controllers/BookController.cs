using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;
using ODataBookStore.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44305/odata/Books";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var list = temp.value;
            List<Book> items = ((JArray)temp.value).Select(x => new Book
            {
                Id = (int)x["Id"],
                ISBN = (string)x["ISBN"],
                Author = (string)x["Author"],
                Title = (string)x["Title"],
                Price = (decimal)x["Price"]
            }).ToList();

            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            Book item = new()
            {
                Id = (int)temp.Id,
                ISBN = (string)temp.ISBN,
                Author = (string)temp.Author,
                Title = (string)temp.Title,
                Price = (decimal)temp.Price
            };

            return View(item);
        }

        public async Task<ActionResult> Create()
        {
            HttpResponseMessage res = await client.GetAsync("https://localhost:44305/odata/Presses");
            string strData = await res.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            //var list = temp.value;
            List<Press> items = ((JArray)temp.value).Select(x => new Press
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"],
                Category = Enum.TryParse((string)x["Category"], out Category category) ? category : default(Category)
            }).ToList();
            ViewData["press"] = new SelectList(items, "Id", "Name");

            res = await client.GetAsync("https://localhost:44305/odata/Addresses");
            strData = await res.Content.ReadAsStringAsync();

            temp = JObject.Parse(strData);
            //var list2 = temp.value;
            List<Address> itemAddresses = ((JArray)temp.value).Select(x => new Address
            {
                Id = (int)x["Id"],
                City = (string)x["City"],
                Street = (string)x["Street"],
            }).ToList();
            ViewData["location"] = new SelectList(itemAddresses, "Id", "City");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book b)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{ProductApiUrl}", b);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            Book item = new()
            {
                Id = (int)temp.Id,
                ISBN = (string)temp.ISBN,
                Author = (string)temp.Author,
                Title = (string)temp.Title,
                Price = (decimal)temp.Price
            };

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book b)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{ProductApiUrl}/{id}", b);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ProductApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            Book item = new()
            {
                Id = (int)temp.Id,
                ISBN = (string)temp.ISBN,
                Author = (string)temp.Author,
                Title = (string)temp.Title,
                Price = (decimal)temp.Price
            };

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{id}");
            return RedirectToAction("Index");
        }
    }
}
