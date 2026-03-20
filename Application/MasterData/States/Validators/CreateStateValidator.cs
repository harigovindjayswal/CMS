using Application.MasterData.States.Commands;
using FluentValidation;

namespace Application.MasterData.States.Validators;

public class CreateStateValidator : AbstractValidator<CreateState.Command>
{
    public CreateStateValidator()
    {
        RuleFor(x => x.State.Name).NotEmpty().MaximumLength(100);
    }
}

