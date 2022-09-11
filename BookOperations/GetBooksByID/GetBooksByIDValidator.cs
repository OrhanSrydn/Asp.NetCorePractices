using book_store.BookOperations.GetBooksByID;
using FluentValidation;

namespace book_store.BookOperations.CreateBooks
{
    public class GetBooksByIDValidator : AbstractValidator<GetBooksByIDCommand>
    {
        public GetBooksByIDValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}