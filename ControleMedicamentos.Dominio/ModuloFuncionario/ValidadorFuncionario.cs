using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
             .NotNull()
             .WithMessage("'Nome' não pode ser nulo.")
             .NotEmpty()
             .WithMessage("'Nome' não pode ser vazio.");

            RuleFor(x => x.Login)
             .NotNull()
             .WithMessage("'Login' não pode ser nulo.")
             .NotEmpty()
             .WithMessage("'Login' não pode ser vazio.");

            RuleFor(x => x.Senha)
            .NotNull()
            .WithMessage("'Senha' não pode ser nulo.")
            .NotEmpty()
            .WithMessage("'Senha' não pode ser vazio.");
        }
    }
}
