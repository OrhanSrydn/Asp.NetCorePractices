using book_store.BookOperations.GetBooks;
using FluentValidation;

namespace book_store.BookOperations.CreateBooks
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(command => command.).GreaterThan(0);
        }
    }
}