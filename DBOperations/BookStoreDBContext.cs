using Microsoft.EntityFrameworkCore;

namespace book_store.DBOperations {

    public class BookStoreDBContext  : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options){}
        
        public DbSet<Book> Books {get;set;}
    }
}