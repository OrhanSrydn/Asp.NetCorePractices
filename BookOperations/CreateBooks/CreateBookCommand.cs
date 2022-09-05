using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get;set;}
        private readonly BookStoreDBContext _dbContext;
        public CreateBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Title == Model.Title);
            if(book is not null){
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }
            book = new Book();
            book.Title = Model.Title;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            book.PublishDate = Model.PublishDate;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int PageCount{get;set;}
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}