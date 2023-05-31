using CQRSMediatrDDD.Domain.Helpers.v1;
using FluentValidation;

namespace CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory")
            .Must(StringHelper.IsCpf).WithMessage("The field {PropertyNae} is note valid for {PropertyName}");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("The field {PropertyName} is not valid")
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
