namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public Paciente(string nome, string cartaoSUS): this()
        {
            Nome = nome;
            CartaoSUS = cartaoSUS;
        }
        public Paciente()
        {

        }

        public Paciente(int v)
        {
        }

        public string Nome { get; set; }
        public string CartaoSUS { get; set; }

    }
}
