using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("'Nome' não pode ser nulo.")
                .NotEmpty()
                .WithMessage("'Nome' não pode ser vazio.");

            RuleFor(x => x.Descricao)
                .NotNull()
                .WithMessage("'Descricao' não pode ser nulo.")
                .NotEmpty()
                 .WithMessage("'Descricao' não pode ser vazia.");

            RuleFor(x => x.Lote)
                .NotNull()
                .WithMessage("'Lote' não pode ser nulo.")
                .NotEmpty()
                 .WithMessage("'Lote' não pode ser vazio.");

            RuleFor(x => x.Validade)
               .NotNull()
               .WithMessage("'Validade' não pode ser nula.")
               .NotEmpty()
                .WithMessage("'Validade' não pode ser vazia.");

            RuleFor(x => x.QuantidadeDisponivel)
               .NotNull()
               .WithMessage("'Quantidade Disponivel' não pode ser nula.")
               .NotEmpty()
                .WithMessage("'Quantidade Disponivel' não pode ser vazia.");
        }
    }
}
