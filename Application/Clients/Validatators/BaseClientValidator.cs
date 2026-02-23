using System;
using Application.Clients.DTO;
using FluentValidation;

namespace Application.Clients.Validatators;

public class BaseClientValidator<T, TDto> : AbstractValidator<T> where TDto : BaseClientDTO
{
    public BaseClientValidator(Func<T,TDto> selector)
    {
        //CascadeMode = CascadeMode.Stop;

        // Name regex: alphabets + space, dot, hyphen
        const string nameRegex = @"^[a-zA-Z][a-zA-Z\s\.\-']*$";

        // Email regex (FluentValidation EmailAddress is usually enough)
        const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        // India Mobile: starts with 6–9 and 10 digits total
        const string mobileRegex = @"^[6-9]\d{9}$";

        // India Pincode: 6 digits
        const string pinCodeRegex = @"^\d{6}$";

        RuleFor(x => selector(x).FirstName)
            .NotEmpty().WithMessage("Please Enter First Name")
            .MaximumLength(50).WithMessage("First Name must not exceed 50 characters")
            .Matches(nameRegex).WithMessage("First Name contains invalid characters");

        RuleFor(x => selector(x).MiddleName)
            .MaximumLength(50).WithMessage("Middle Name must not exceed 50 characters")
            .Matches(nameRegex)
            .When(x => !string.IsNullOrWhiteSpace(selector(x).MiddleName))
            .WithMessage("Middle Name contains invalid characters");

        RuleFor(x => selector(x).LastName)
            .NotEmpty().WithMessage("Please Enter Last Name")
            .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters")
            .Matches(nameRegex).WithMessage("Last Name contains invalid characters");

        RuleFor(x => selector(x).EmailId)
            .NotEmpty().WithMessage("Please Enter Email Id")
            .MaximumLength(100).WithMessage("Email Id must not exceed 100 characters")
            .Matches(emailRegex).WithMessage("Please Enter a Valid Email Id");

        RuleFor(x => selector(x).MobileNo)
            .NotEmpty().WithMessage("Please Enter Mobile No")
            .Matches(mobileRegex).WithMessage("Please Enter a Valid 10-digit Mobile No");

        RuleFor(x => selector(x).Address)
            .NotEmpty().WithMessage("Please Enter Address")
            .MinimumLength(5).WithMessage("Address is too short")
            .MaximumLength(250).WithMessage("Address must not exceed 250 characters");

        RuleFor(x => selector(x).State)
            .NotNull().WithMessage("Please Select State");

        RuleFor(x => selector(x).District)
            .NotNull().WithMessage("Please Select District");

        RuleFor(x => selector(x).City)
            .NotNull().WithMessage("Please Select City");

        RuleFor(x => selector(x).PinCode)
            .NotEmpty().WithMessage("Please Enter Pin Code")
            .Matches(pinCodeRegex).WithMessage("Please Enter a Valid 6-digit Pin Code");

        RuleFor(x => selector(x).Notes)
            .NotEmpty().WithMessage("Please Enter Notes")
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters");
    }
}