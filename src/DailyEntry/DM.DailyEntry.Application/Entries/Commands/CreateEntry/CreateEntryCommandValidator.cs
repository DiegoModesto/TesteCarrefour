using FluentValidation;

namespace DM.Application.Entries.Commands.CreateEntry;

public sealed class CreateEntryCommandValidator : AbstractValidator<CreateEntryCommand>
{
    public CreateEntryCommandValidator()
    {
        RuleFor(x => x.Balance)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Type)
            .NotEmpty()
            .NotNull();
    }
}
