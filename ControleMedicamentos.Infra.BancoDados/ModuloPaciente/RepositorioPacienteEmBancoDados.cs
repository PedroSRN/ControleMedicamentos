using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDados
    {
        private const string enderecoBanco = 
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleMedicamentosDB;Integrated Security=True;
            Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBPACIENTE]
             (
                [NOME],
                [CARTAOSUS]
             )     
             VALUES
             (
	            @NOME,
	            @CARTAOSUS    
	         );   SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
            @"UPDATE [TBPACIENTE]
                SET
                       [NOME] = @NOME,
	                   [CARTAOSUS] = @CARTAOSUS
                WHERE
                    [ID] = @ID;";


        private const string sqlExcluir =
            @"DELETE FROM[TBPACIENTE]
                  WHERE
                       [ID] = @ID;";


        private const string sqlSelecionarTodos =
            @"SELECT 
                  [ID],
                  [NOME],
	              [CARTAOSUS]
             FROM 
                 [TBPACIENTE];";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]
		        WHERE
                    [ID] = @ID;";

        #endregion


        public ValidationResult Inserir(Paciente novoPaciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(novoPaciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosPaciente(novoPaciente, cmdInserir);
            conexao.Open();

            var id = cmdInserir.ExecuteScalar();

            novoPaciente.Id = Convert.ToInt32(id);
            conexao.Close();
            return resultadoValidacao;
        }

        public ValidationResult Editar(Paciente paciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Paciente paciente)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Id", paciente.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Paciente> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente= comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
            {
                Paciente paciente = ConverterParaPaciente(leitorPaciente);

                pacientes.Add(paciente);
            }

            conexaoComBanco.Close();

            return pacientes;
        }

        public Paciente SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente paciente = null;
            if (leitorPaciente.Read())
                paciente = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return paciente;
        }

        private Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaoSUS = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            return new Paciente
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaoSUS,
            };

            
        }

        private static void ConfigurarParametrosPaciente(Paciente novoPaciente, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("ID", novoPaciente.Id);
            cmdInserir.Parameters.AddWithValue("NOME", novoPaciente.Nome);
            cmdInserir.Parameters.AddWithValue("CARTAOSUS", novoPaciente.CartaoSUS);
           
        }
    }
}
