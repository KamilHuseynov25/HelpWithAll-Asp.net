namespace HelpWithAll.Core.Validators;
using FluentValidation;

using HelpWithAll.Core.Models;

    public class HelperValidator : AbstractValidator<Helper>
    {
        public HelperValidator()
        {
            RuleFor(helper => helper.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(2, 50)
                .WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(helper => helper.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.");

            RuleFor(helper => helper.Profession)
                .NotEmpty().WithMessage("Profession is required.")
                .Length(2, 100).WithMessage("Profession must be between 2 and 100 characters.");

            RuleFor(helper => helper.PaymentPerHour)
                .GreaterThan(0).WithMessage("Payment per hour must be greater than 0.");

            RuleFor(helper => helper.Age)
                .GreaterThan(18).WithMessage("Age must be greater than 18.");

            RuleFor(helper => helper.Experience)
                .GreaterThanOrEqualTo(0).WithMessage("Experience must be 0 or greater.");

            RuleFor(helper => helper.Avalibility)
                .NotNull().WithMessage("Availability is required.");

            RuleFor(helper => helper.Rating)
            .GreaterThan(0)
            .LessThan(5.0)
            .WithMessage("Rating must be between 0 and 5.");
        }
    }
