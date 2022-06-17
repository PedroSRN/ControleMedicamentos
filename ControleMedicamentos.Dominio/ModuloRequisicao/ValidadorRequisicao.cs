using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                .NotNull()
                .WithMessage("'Medicamento' não pode ser nulo.")
                .NotEmpty()
                .WithMessage("'Medicamento' não pode ser vazio.");

            RuleFor(x => x.Paciente)
               .NotNull()
               .WithMessage("'Paciente' não pode ser nulo.")
               .NotEmpty()
               .WithMessage("'Paciente' não pode ser vazio.");

            RuleFor(x => x.QtdMedicamento)
               .NotNull()
               .WithMessage("'Quantidade de Medicamento' não pode ser nula.")
               .NotEmpty()
               .WithMessage("'Quantidade de Medicamento' não pode ser vazia.");

            RuleFor(x => x.Data)
               .NotNull()
               .WithMessage("'Data' não pode ser nula.")
               .NotEmpty()
               .WithMessage("'Data' não pode ser vazia.");

            RuleFor(x => x.Funcionario)
              .NotNull()
              .WithMessage("'Funcionario' não pode ser nulo.")
              .NotEmpty()
              .WithMessage("'Funcionario' não pode ser vazio.");


        }
    }
}
