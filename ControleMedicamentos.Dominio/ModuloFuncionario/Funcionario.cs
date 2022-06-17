namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {

        public Funcionario(string nome, string login, string senha) :this()
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public Funcionario()
        {
                
        }

        public Funcionario(int v)
        {
        }

        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
