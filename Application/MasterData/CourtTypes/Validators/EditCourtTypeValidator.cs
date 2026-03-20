using Application.MasterData.CourtTypes.Commands;
using FluentValidation;

namespace Application.MasterData.CourtTypes.Validators;

public class EditCourtTypeValidator : AbstractValidator<EditCourtType.Command>
{
    public EditCourtTypeValidator()
    {
        RuleFor(x => x.CourtType.CourtTypeId).GreaterThan(0);
        RuleFor(x => x.CourtType.TypeName).NotEmpty().MaximumLength(120);
    }
}

