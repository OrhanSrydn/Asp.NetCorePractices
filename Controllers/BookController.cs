using book_store.BookOperations.GetBookById;
using book_store.BookOperations.GetBooks;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static book_store.BookOperations.GetBooks.CreateBookCommand;
using static book_store.BookOperations.GetBooks.UpdateBookCommand;

namespace book_store.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : Controller
    {
        private readonly BookStoreDBContext _context;

        public BookController(BookStoreDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetBooks(){
        GetBooksQuery query = new GetBooksQuery(_context);
        var result = query.Handle();
        return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookById getbyid = new GetBookById(_context);
            try
            {
                var result = getbyid.Handle(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public Book Get([FromQuery]string id){
        //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] UpdateBookModel updatedBook){
            UpdateBookCommand updateCommand = new UpdateBookCommand(_context);
            try
            {
                updateCommand.Model = updatedBook;
                updateCommand.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null){
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}