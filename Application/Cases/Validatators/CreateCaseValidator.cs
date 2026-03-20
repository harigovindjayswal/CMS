using Application.Cases.Commands;
using FluentValidation;

namespace Application.Cases.Validatators;

public class CreateCaseValidator : AbstractValidator<CreateCase.Command>
{
    public CreateCaseValidator()
    {
        RuleFor(x => x.Case.LawyerRequestId).GreaterThan(0);
        RuleFor(x => x.Case.CaseTypeId).GreaterThan(0);
        RuleFor(x => x.Case.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Case.Description).MaximumLength(2000);
        RuleFor(x => x.Case.CaseNumber).MaximumLength(100);
        RuleFor(x => x.Case.Purpose).MaximumLength(250);

        RuleFor(x => x.Case)
            .Must(c => (c.CourtId.HasValue && c.CourtId.Value > 0) || !string.IsNullOrWhiteSpace(c.CourtName))
            .WithMessage("CourtId or CourtName is required");
    }
}
