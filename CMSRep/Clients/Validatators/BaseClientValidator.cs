using System;
using CMSApplication.Clients.DTO;
using FluentValidation;

namespace CMSApplication.Clients.Validatators;

public class BaseClientValidator<T, TDto> : AbstractValidator<T> where TDto : BaseClientDTO
{
    public BaseClientValidator(Func<T,TDto> selector)
    {
        // RuleFor(x => selector(x).ClientId)
        //     .NotNull().WithMessage("Please Enter Client Id");

        RuleFor(x => selector(x).UserId)
            .NotEmpty().WithMessage("Please Enter User Id");
        RuleFor(x => selector(x).FirstName)
            .NotEmpty().WithMessage("Please Enter First Name");

        RuleFor(x => selector(x).MiddleName)
            .NotEmpty().WithMessage("Please Enter Middle Name");
        RuleFor(x => selector(x).LastName)
            .NotEmpty().WithMessage("Please Enter Last Name");
        RuleFor(x => selector(x).EmailId)
            .NotEmpty().WithMessage("Please Enter Email Id")
            .EmailAddress().WithMessage("Please Enter Valid Email Id");
        RuleFor(x => selector(x).MobileNo)
            .NotEmpty().WithMessage("Please Enter Mobile No");
        RuleFor(x => selector(x).Address)
            .NotEmpty().WithMessage("Please Enter Address");
        RuleFor(x => selector(x).State)
            .NotNull().WithMessage("Please Select State");
        RuleFor(x => selector(x).District)
            .NotNull().WithMessage("Please Select District");
        RuleFor(x => selector(x).City)
            .NotNull().WithMessage("Please Select City");
        RuleFor(x => selector(x).PinCode)
            .NotEmpty().WithMessage("Please Enter Pin Code");
        RuleFor(x => selector(x).Notes)
            .NotEmpty().WithMessage("Please Enter Notes");

        RuleFor(x => selector(x).IsActive)
            .NotNull().WithMessage("Please Select Active Status");
    }
}