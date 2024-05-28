//using EBook.Model.BookModels;
//using FluentValidation;

//namespace EBook.Validators
//{
//    public class BookValidators : AbstractValidator<BookDto>
//    {
//        public BookValidators()
//        {


//            RuleFor(p => p.Title)
//                .Cascade(CascadeMode.StopOnFirstFailure)
//                .NotEmpty().WithMessage("Title should not be empty.")
//                .Length(2, 20)
//                .WithMessage("Please enter a valid title.");


//            RuleFor(p => p.Description)
//            .MaximumLength(500).WithMessage("Description should not exceed 30 characters.");

//            RuleFor(p => p.ISBN)
//                .NotEmpty().WithMessage("ISBN should not be empty.")
//                .Length(10, 13).WithMessage("ISBN should not exceed 20 characters.");

//            RuleFor(p => p.PublicationDate)
//                .NotEmpty().WithMessage("Publication date should not be empty.")
//                .Must(BeValidDate).WithMessage("Invalid publication date.");

//            RuleFor(p => p.Price)
//                .NotEmpty().WithMessage("Price should not be empty.")
//                .GreaterThan(0).WithMessage("Price should be greater than 0.");

//            RuleFor(p => p.Language)
//                .NotEmpty().WithMessage("Language should not be empty.");

//            RuleFor(p => p.Publisher)
//                .NotEmpty().WithMessage("Publisher should not be empty.");

//            RuleFor(p => p.PageCount)
//                .NotEmpty().WithMessage("Page count should not be empty.")
//                .GreaterThan(0).WithMessage("Page count should be greater than 0.");

//            RuleFor(p => p.AverageRating)
//                .InclusiveBetween(0, 5).WithMessage("Average rating should be between 0 and 5.");
//        }

//        private bool isValidString(string value)
//        {
//            foreach (char c in value)
//            {
//                // if the are symbols or whitespaces => invalid
//                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
//                {
//                    return false;
//                }
//            }
//            return true;
//        }

//        private bool BeValidDate(DateTime date)
//        {
//            int minYear = 1900;
//            int maxYear = DateTime.Now.Year;
//            return (date.Year >= minYear && date.Year <= maxYear);
//        }
//    }
//}
