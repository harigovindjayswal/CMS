using Application.Cases.Commands;
using FluentValidation;

namespace Application.Cases.Validatators;

public class AddCaseNoteValidator : AbstractValidator<AddCaseNote.Command>
{
    public AddCaseNoteValidator()
    {
        RuleFor(x => x.Note.CaseId).GreaterThan(0);
        RuleFor(x => x.Note.Content).NotEmpty().MaximumLength(2000);
    }
}

