using Application.MasterData.CourtTypes.Commands;
using FluentValidation;

namespace Application.MasterData.CourtTypes.Validators;

public class CreateCourtTypeValidator : AbstractValidator<CreateCourtType.Command>
{
    public CreateCourtTypeValidator()
    {
        RuleFor(x => x.CourtType.TypeName).NotEmpty().MaximumLength(120);
    }
}

