using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.Data;
using System.Linq;

namespace ODataBookStore.Controllers
{
    public class PressesController : ODataController
    {
        private BookStoreContext _db;

        public PressesController(BookStoreContext db)
        {
            _db = db;

            if (db.Books.Count() == 0)
            {
                foreach (var item in DataSource.GetBooks())
                {
                    db.Books.Add(item);
                    db.Presses.Add(item.Press);
                    db.Addresses.Add(item.Location);
                }
                db.SaveChanges();
            }
        }

        [EnableQuery]
        public IActionResult Get() => Ok(_db.Presses);
    }
}
