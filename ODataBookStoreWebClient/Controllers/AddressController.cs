using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ODataBookStoreWebClient.Controllers
{
    public class AddressController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public AddressController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44305/odata/Addresses";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage res = await client.GetAsync(ProductApiUrl);
            string strData = await res.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<Address> items = ((JArray)temp.value).Select(x => new Address
            {
                Id = (int)x["Id"],
                City = (string)x["City"],
                Street = (string)x["Street"]
            }).ToList();

            return View(items);
        }
    }
}
