using Application.MasterData.Cities.Commands;
using FluentValidation;

namespace Application.MasterData.Cities.Validators;

public class CreateCityValidator : AbstractValidator<CreateCity.Command>
{
    public CreateCityValidator()
    {
        RuleFor(x => x.City.DistrictId).GreaterThan(0);
        RuleFor(x => x.City.Name).NotEmpty().MaximumLength(100);
    }
}

