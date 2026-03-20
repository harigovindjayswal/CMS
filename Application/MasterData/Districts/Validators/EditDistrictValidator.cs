using Application.MasterData.Districts.Commands;
using FluentValidation;

namespace Application.MasterData.Districts.Validators;

public class EditDistrictValidator : AbstractValidator<EditDistrict.Command>
{
    public EditDistrictValidator()
    {
        RuleFor(x => x.District.DistrictId).GreaterThan(0);
        RuleFor(x => x.District.StateId).GreaterThan(0);
        RuleFor(x => x.District.Name).NotEmpty().MaximumLength(100);
    }
}

