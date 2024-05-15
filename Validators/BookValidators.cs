using EBook.Model.BookModels;
using FluentValidation;

namespace EBook.Validators
{
    public class BookValidators: AbstractValidator<Book>
    {
        public BookValidators()
        {
            RuleFor(p => p.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Title should not be empty.")
                .Length(2, 20)
                .WithMessage("Please enter a valid title.")
                .Must(isValidString).WithMessage("Title contains Invalid Characters");
        }

        protected bool isValidString(string value)
        {
            value = value.Replace(" ", "");
            value = value.Replace("-", "");
            return value.All(Char.IsLetter);
        }
    }
}
