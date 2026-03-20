using Application.MasterData.States.Commands;
using FluentValidation;

namespace Application.MasterData.States.Validators;

public class EditStateValidator : AbstractValidator<EditState.Command>
{
    public EditStateValidator()
    {
        RuleFor(x => x.State.StateId).GreaterThan(0);
        RuleFor(x => x.State.Name).NotEmpty().MaximumLength(100);
    }
}

