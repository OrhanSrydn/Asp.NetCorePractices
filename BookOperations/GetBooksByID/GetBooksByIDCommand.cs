using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.GetBooksByID
{
    public class GetBooksByIDCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int Id { get; set; }
        public GetBooksByIDCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            BookDetailViewModel vm = new BookDetailViewModel();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı.");
            }
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.DateTime = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string DateTime { get; set; }
    }
}