using AutoMapper;
using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get;set;}
        private readonly BookStoreDBContext _dbContext;

        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Title == Model.Title);
            if(book is not null){
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }
            book = _mapper.Map<Book>(Model);
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