using System.Collections.Generic;
using ODataBookStore.Models;

namespace ODataBookStore.Data
{
    public static class DataSource
    {
        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (listBooks != null)
            {
                return listBooks;
            }

            listBooks = new List<Book>();
            Book book = new()
            {
                Id = 1,
                ISBN = "123-4-567-8912-1",
                Title = "Essential C# 5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address
                {
                    Id = 1,
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book
            {
                Id = 2,
                ISBN = "321-1-111-22222-2",
                Title = "Spider Games",
                Author = "Peter",
                Price = 11.11m,
                Location = new Address
                {
                    Id = 2,
                    City = "HN City",
                    Street = "Cau, Giay"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Peterrr",
                    Category = Category.EBook
                }
            };
            listBooks.Add(book);

            book = new Book
            {
                Id = 3,
                ISBN = "333-3-333-33333-3",
                Title = "ASP .Net 7",
                Author = "Mircosoft",
                Price = 99.99m,
                Location = new Address
                {
                    Id = 3,
                    City = "Da Nang City",
                    Street = "Cau Rong"
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "NOD",
                    Category = Category.Magazine
                }
            };
            listBooks.Add(book);

            return listBooks;
        }
    }
}
