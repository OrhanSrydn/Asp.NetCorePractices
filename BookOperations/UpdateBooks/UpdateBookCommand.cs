using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.GetBooks
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model {get;set;}
        private readonly BookStoreDBContext _dbcontext;
        public UpdateBookCommand(BookStoreDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbcontext.SaveChanges();
        }

         public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}