using System;
using CMSApplication.Clients.Commands;
using CMSApplication.Clients.DTO;
using FluentValidation;

namespace CMSApplication.Clients.Validatators;

public class EditClientValidator : BaseClientValidator<EditClient.Command, EditClientDTO>
{
    public EditClientValidator() : base(x => x.client)
    {
        RuleFor(x => x.client.ClientId)
                   .NotEmpty().WithMessage("Id is required")
                   .GreaterThan(0).WithMessage("Id must be a positive integer");
        RuleFor(x => x.client.IsActive)
           .NotNull().WithMessage("Please Select Active Status");
    }
}