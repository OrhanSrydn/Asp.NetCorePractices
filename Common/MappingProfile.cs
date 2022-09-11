using AutoMapper;
using book_store.BookOperations.CreateBooks;
using book_store.BookOperations.GetBooks;
using book_store.BookOperations.GetBooksByID;

namespace book_store.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
        }
    }
}