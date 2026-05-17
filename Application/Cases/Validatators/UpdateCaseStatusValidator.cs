using Application.Cases.Commands;
using FluentValidation;

namespace Application.Cases.Validatators;

public class UpdateCaseStatusValidator : AbstractValidator<UpdateCaseStatus.Command>
{
    public UpdateCaseStatusValidator()
    {
        RuleFor(x => x.Status.CaseId).GreaterThan(0);

        RuleFor(x => x.Status.Status)
    .IsInEnum()
    .WithMessage("Invalid case status");

        RuleFor(x => x.Status.Stage)
            .IsInEnum()
            .WithMessage("Invalid case stage");
    }
}

