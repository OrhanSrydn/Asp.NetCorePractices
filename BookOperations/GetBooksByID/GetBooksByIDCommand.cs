using AutoMapper;
using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.GetBooksByID
{
    public class GetBooksByIDCommand
    {
        private readonly BookStoreDBContext _dbContext;

        private readonly IMapper _mapper;
        public int Id { get; set; }
        public GetBooksByIDCommand(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            BookDetailViewModel vm =  _mapper.Map<BookDetailViewModel>(book);
            if(book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı.");
            }
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