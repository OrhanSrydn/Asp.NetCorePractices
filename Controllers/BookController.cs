using book_store.BookOperations.GetBooks;
using book_store.BookOperations.CreateBooks;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;
using book_store.BookOperations.UpdateBooks;
using book_store.BookOperations.GetBooksByID;
using book_store.BookOperations.DeleteBooks;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace book_store.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : Controller
    {
        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public BookController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            GetBooksByIDCommand cmd = new GetBooksByIDCommand(_context, _mapper);
            GetBooksByIDValidator validator = new GetBooksByIDValidator();
            BookDetailViewModel result;
            try
            {
                cmd.Id = id;
                ValidationResult vr = validator.Validate(cmd);
                validator.ValidateAndThrow(cmd);
                result = cmd.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
            
        }

        // [HttpGet]
        // public Book Get([FromQuery]string id){
        //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand cmd = new CreateBookCommand(_context, _mapper);
                cmd.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                ValidationResult result = validator.Validate(cmd);
                validator.ValidateAndThrow(cmd);
                cmd.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook){
            UpdateBookCommand ubc = new UpdateBookCommand(_context);
            try
            {
                ubc.Model = updatedBook;
                ubc.Id = id;
                UpdateBookCommandValidator ubcv = new UpdateBookCommandValidator();
                ValidationResult result = ubcv.Validate(ubc);
                ubcv.ValidateAndThrow(ubc);
                ubc.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            try
            {
                DeleteBookCommand dbc = new DeleteBookCommand(_context);
                dbc.Id = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                ValidationResult result = validator.Validate(dbc);
                validator.ValidateAndThrow(dbc);
                dbc.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}