using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados
    {
        private const string enderecoBanco =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleMedicamentosDB;Integrated Security=True;
            Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR]
            (
               [NOME],
               [TELEFONE],
               [EMAIL],
               [CIDADE],
               [ESTADO]
            )
            VALUES
            (
		       @NOME,
			   @TELEFONE,
			   @EMAIL,
			   @CIDADE,    
			   @ESTADO
		    );SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
            @"UPDATE [TBFORNECEDOR]
                SET  
                    [NOME] = @NOME,
                    [TELEFONE] = @TELEFONE,
                    [EMAIL] = @EMAIL,
                    [CIDADE] = @CIDADE,
                    [ESTADO] = @ESTADO
                WHERE
                     [ID] = @ID;";


        private const string sqlExcluir =
            @"DELETE FROM[TBFORNECEDOR]
                WHERE
                     [ID] = @ID; ";


        private const string sqlSelecionarTodos =
            @"SELECT
                   [ID],
                   [NOME],
                   [TELEFONE],
                   [EMAIL],
                   [CIDADE],
                   [ESTADO]
             FROM
                 [TBFORNECEDOR]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [ID], 
		            [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
	            FROM 
		            [TBFORNECEDOR]
		        WHERE
                    [ID] = @ID";
        #endregion

        public ValidationResult Inserir(Fornecedor novoFornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(novoFornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosFornecedor(novoFornecedor, cmdInserir);
            conexao.Open();

            var id = cmdInserir.ExecuteScalar();

            novoFornecedor.Id = Convert.ToInt32(id);
            conexao.Close();
            return resultadoValidacao;
        }

        public ValidationResult Editar(Fornecedor fornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(fornecedor, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Id", fornecedor.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaFornecedor(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;
        }

        public Fornecedor SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;
            if (leitorFornecedor.Read())
                fornecedor = ConverterParaFornecedor(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;
        }



        private Fornecedor ConverterParaFornecedor(SqlDataReader leitorFornecedor)
        {
            int id = Convert.ToInt32(leitorFornecedor["ID"]);
            string nome = Convert.ToString(leitorFornecedor["NOME"]);
            string telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            string email = Convert.ToString(leitorFornecedor["EMAIL"]);
            string cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            string estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            var paciente = new Fornecedor
            {
                Id = id,
                Nome = nome,
                Telefone = telefone,
                Email = email,
                Cidade = cidade,
                Estado = estado
               
            };

            return paciente;
        }

        private static void ConfigurarParametrosFornecedor(Fornecedor novoFornecedor, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("ID", novoFornecedor.Id);
            cmdInserir.Parameters.AddWithValue("NOME", novoFornecedor.Nome);
            cmdInserir.Parameters.AddWithValue("TELEFONE", novoFornecedor.Telefone);
            cmdInserir.Parameters.AddWithValue("EMAIL", novoFornecedor.Email);
            cmdInserir.Parameters.AddWithValue("CIDADE", novoFornecedor.Cidade);
            cmdInserir.Parameters.AddWithValue("ESTADO", novoFornecedor.Estado);

        }
    }
}
