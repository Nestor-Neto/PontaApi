

using Domain.Entities;
using FluentValidation;

namespace Servico.Validadores
{
    public class ValidatorTarefas : AbstractValidator<Tarefas>
    {
        public ValidatorTarefas()
        {
            
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!")
                .MaximumLength(90).WithMessage("Campo comporta até 90 caracteres.");

            RuleFor(c => c.Descricao)
                 .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!")
                .MaximumLength(200).WithMessage("Campo comporta até 200 caracteres.");

            RuleFor(c => c.Data)
                .NotEmpty().WithMessage("Campo obrigatório!")
                .NotNull().WithMessage("Campo obrigatório!");

            RuleFor(c => c.Status)
                .NotNull().WithMessage("Campo obrigatório! Utilize: \n 0 - Pendente \n 1 - Em Conclusão \n 2 - Concluido.")
             .IsInEnum().WithMessage("Status incorreto.");

        }
    }

}
