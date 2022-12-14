using System;
using Microsoft.EntityFrameworkCore;

namespace book_store.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider ServiceProvider)
        {
            using (var context = new BookStoreDBContext(ServiceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book
                    {
                        Id = 1, //
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Id = 2, //
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Id = 3, //
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 05, 23)
                    });
                context.SaveChanges();
            }
        }
    }
}