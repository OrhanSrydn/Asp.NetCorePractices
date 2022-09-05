using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _dbContext;

        public int Id { get; set; }
        public UpdateBookModel Model {get;set;}
        public UpdateBookCommand(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id == Id);
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.Update(book);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
    }
}