using Application.MasterData.Courts.Commands;
using FluentValidation;

namespace Application.MasterData.Courts.Validators;

public class CreateCourtValidator : AbstractValidator<CreateCourt.Command>
{
    public CreateCourtValidator()
    {
        RuleFor(x => x.Court.CourtTypeId).GreaterThan(0);
        RuleFor(x => x.Court.CityId).GreaterThan(0);
        RuleFor(x => x.Court.Name).NotEmpty().MaximumLength(200);
    }
}

