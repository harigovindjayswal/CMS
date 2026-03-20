using Application.MasterData.Cities.Commands;
using FluentValidation;

namespace Application.MasterData.Cities.Validators;

public class EditCityValidator : AbstractValidator<EditCity.Command>
{
    public EditCityValidator()
    {
        RuleFor(x => x.City.CityId).GreaterThan(0);
        RuleFor(x => x.City.DistrictId).GreaterThan(0);
        RuleFor(x => x.City.Name).NotEmpty().MaximumLength(100);
    }
}

