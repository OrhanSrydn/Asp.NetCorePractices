using book_store.Common;
using book_store.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace book_store.BookOperations.DeleteBooks
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int Id { get; set; }
        public DeleteBookCommand(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

    }
}
