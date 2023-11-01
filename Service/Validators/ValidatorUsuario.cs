

using Domain.Entities;
using FluentValidation;

namespace Service.Validador
{


    public class ValidatorUsuario : AbstractValidator<Usuario>
    {
        public ValidatorUsuario()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!")
                .MaximumLength(100).WithMessage("Campo comporta até 90 caracteres.");

            RuleFor(c => c.Login)
                .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!")
                .MaximumLength(30).WithMessage("Campo comporta até 30 caracteres.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!")
                .MaximumLength(30).WithMessage("Campo comporta até 30 caracteres.");
        }
    }

}
