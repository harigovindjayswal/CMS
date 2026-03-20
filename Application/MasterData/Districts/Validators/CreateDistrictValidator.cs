using Application.MasterData.Districts.Commands;
using FluentValidation;

namespace Application.MasterData.Districts.Validators;

public class CreateDistrictValidator : AbstractValidator<CreateDistrict.Command>
{
    public CreateDistrictValidator()
    {
        RuleFor(x => x.District.StateId).GreaterThan(0);
        RuleFor(x => x.District.Name).NotEmpty().MaximumLength(100);
    }
}

