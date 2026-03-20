using Application.LawyerAdmin.Commands;
using FluentValidation;

namespace Application.LawyerAdmin.Validators;

public class CreateCaseForRequestValidator : AbstractValidator<CreateCaseForRequest.Command>
{
    public CreateCaseForRequestValidator()
    {
        RuleFor(x => x.Case.LawyerRequestId).GreaterThan(0);
        RuleFor(x => x.Case.CaseTypeId).GreaterThan(0);
        RuleFor(x => x.Case.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Case.CourtName).MaximumLength(200);
        RuleFor(x => x.Case.CaseNumber).MaximumLength(100);
        RuleFor(x => x.Case.Purpose).MaximumLength(300);
        RuleFor(x => x.Case.Description).MaximumLength(2000);
        RuleFor(x => x.Case.CourtId).GreaterThan(0).When(x => x.Case.CourtId.HasValue);
    }
}

