namespace HelpWithAll.Core.Validators;
using FluentValidation;
using HelpWithAll.Core.Models;


    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(customer => customer.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.");

            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(customer => customer.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
        }
    }


