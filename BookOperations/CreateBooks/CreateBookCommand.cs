using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.GetBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get; set;}
        private readonly BookStoreDBContext _dbcontext;
        public CreateBookCommand(BookStoreDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(book => book.Title == Model.Title);
            if(book is not null){
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;
            
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}