using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
            .NotNull()
            .WithMessage("'Nome' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Nome' não pode ser vazio.");

            RuleFor(x => x.Telefone)
            .NotNull()
            .WithMessage("'Telefone' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Telefone' não pode ser vazio.");

            RuleFor(x => x.Email)
            .NotNull()
            .WithMessage("'Email' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Email' não pode ser vazio.");
            
            RuleFor(x => x.Cidade)
            .NotNull()
            .WithMessage("'Cidade' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Cidade' não pode ser vazio.");
            
            RuleFor(x => x.Estado)
            .NotNull()
            .WithMessage("'Estado' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Estado' não pode ser vazio.");
        }
    }
}
