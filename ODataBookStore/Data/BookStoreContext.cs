using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;

namespace ODataBookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
            //modelBuilder.Entity<Address>().OwnsOne(c => c.City);
        }
    }
}
