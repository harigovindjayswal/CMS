using Application.LawyerRequests.Commands;
using FluentValidation;

namespace Application.LawyerRequests.Validators;

public class CreateLawyerRequestValidator : AbstractValidator<CreateLawyerRequest.Command>
{
    public CreateLawyerRequestValidator()
    {
        RuleFor(x => x.Request.LawyerId).GreaterThan(0);
        RuleFor(x => x.Request.CaseTypeId).GreaterThan(0);
        RuleFor(x => x.Request.StateId).GreaterThan(0);
        RuleFor(x => x.Request.DistrictId).GreaterThan(0);
        RuleFor(x => x.Request.CityId).GreaterThan(0);
        RuleFor(x => x.Request.CaseDescription).NotEmpty().MaximumLength(2000);
    }
}

