using Application.LawyerRequests.Commands;
using Domain.AppEntities;
using FluentValidation;

namespace Application.LawyerRequests.Validators;

public class RespondToLawyerRequestValidator : AbstractValidator<RespondToLawyerRequest.Command>
{
    public RespondToLawyerRequestValidator()
    {
        RuleFor(x => x.Response.LawyerRequestId).GreaterThan(0);
        RuleFor(x => x.Response.LawyerRemark).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Response.Status).Must(s => s == LawyerRequestStatus.Accepted || s == LawyerRequestStatus.Rejected)
            .WithMessage("Status must be Accepted or Rejected");
    }
}

