using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.GetBookById
{
    public class GetBookById
    {
        private readonly BookStoreDBContext _dbContext;
        public GetBookById(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public Book Handle(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id == id).Single();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            else
            {
                return book;
            }
        }

        public class GetBookByIdModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}