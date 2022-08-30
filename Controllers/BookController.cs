using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

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
        public List<Book> GetBooks(){
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id){
            var book = _context.Books.Where(x => x.Id == id).Single();
            return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery]string id){
        //     var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook){
            var book = _context.Books.SingleOrDefault(book => book.Title == newBook.Title);
            if(book is not null){
                return BadRequest();
            }
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book is null){
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
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