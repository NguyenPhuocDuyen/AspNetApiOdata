using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Data;
using ODataBookStore.Models;
using System.Linq;

namespace ODataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext _db;

        public BooksController(BookStoreContext db)
        {
            _db = db;
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

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

        [EnableQuery(PageSize = 9)]
        public IActionResult Get() => Ok(_db.Books);

        [EnableQuery]
        public IActionResult Get(int key, string version) => Ok(_db.Books.FirstOrDefault(c => c.Id == key));

        [EnableQuery]
        public IActionResult Post([FromBody]Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }

        [EnableQuery]
        public IActionResult Delete(int key)
        {
            Book b = _db.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }

            _db.Books.Remove(b);
            _db.SaveChanges();
            return Ok();
        }

        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Book book)
        {
            var currentBook = _db.Books.FirstOrDefault(b => b.Id == key);
            if (currentBook == null)
            {
                return NotFound();
            }
            _db.Books.Update(book);
            _db.SaveChanges();
            return Ok();
        }
    }
}
