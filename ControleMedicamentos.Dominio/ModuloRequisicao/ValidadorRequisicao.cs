using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            //RuleFor(x => x.Nome).NotNull().NotEmpty();
        }
    }
}
