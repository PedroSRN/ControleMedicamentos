using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados
    {
        private const string enderecoBanco =
           @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleMedicamentosDB;Integrated Security=True;
            Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO]
            (
            			[FUNCIONARIO_ID],
                        [PACIENTE_ID],
                        [MEDICAMENTO_ID],
                        [QUANTIDADEMEDICAMENTO],
                        [DATA]
            )
                 VALUES(
                        @FUNCIONARIO_ID,
                        @PACIENTE_ID,
                        @MEDICAMENTO_ID,
                        @QUANTIDADEMEDICAMENTO,
                        @DATA
            );SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
            @"UPDATE [TBREQUISICAO] 
                SET
                    [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
                    [PACIENTE_ID] = @PACIENTE_ID,
                    [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                    [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
                    [DATA] = @DATA
                WHERE
                     [ID] = @ID;";


        private const string sqlExcluir = 
            @"DELETE FROM[TBREQUISICAO]
                WHERE
                     [ID] = @ID;";


        private const string sqlSelecionarTodos =
            @"SELECT  
                    REQUISICAO.ID,
                    REQUISICAO.QUANTIDADEMEDICAMENTO,
                    REQUISICAO.[DATA],
		
		            FUNCIONARIO.ID AS ID_FUNCIONARIO,
		            FUNCIONARIO.NOME AS NOME_FUNCIONARIO,
		
		            PACIENTE.ID AS ID_PACIENTE,
		            PACIENTE.NOME AS NOME_PACIENTE,
		            PACIENTE.CARTAOSUS,
		
		            MEDICAMENTO.ID AS ID_MEDICAMENTO,
		            MEDICAMENTO.NOME AS NOME_MEDICAMENTO,
		            MEDICAMENTO.LOTE

              FROM 
                  TBREQUISICAO AS REQUISICAO INNER JOIN TBFUNCIONARIO AS FUNCIONARIO
	             ON
	             REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID INNER JOIN TBPACIENTE AS PACIENTE
	             ON
	             REQUISICAO.PACIENTE_ID = PACIENTE.ID INNER JOIN TBMEDICAMENTO AS MEDICAMENTO 
	             ON
	             REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID";


        private const string sqlSelecionarPorNumero =
            @"SELECT  
	                    REQUISICAO.ID,
                        REQUISICAO.QUANTIDADEMEDICAMENTO,
                        REQUISICAO.[DATA],
		
		                FUNCIONARIO.ID AS ID_FUNCIONARIO,
		                FUNCIONARIO.NOME AS NOME_FUNCIONARIO,
		
		                PACIENTE.ID AS ID_PACIENTE,
		                PACIENTE.NOME AS NOME_PACIENTE,
		                PACIENTE.CARTAOSUS,
		
		                MEDICAMENTO.ID AS ID_MEDICAMENTO,
		                MEDICAMENTO.NOME AS NOME_MEDICAMENTO,
		                MEDICAMENTO.LOTE

                  FROM 
                      TBREQUISICAO AS REQUISICAO INNER JOIN TBFUNCIONARIO AS FUNCIONARIO
	                 ON
	                 REQUISICAO.FUNCIONARIO_ID = FUNCIONARIO.ID INNER JOIN TBPACIENTE AS PACIENTE
	                 ON
	                 REQUISICAO.PACIENTE_ID = PACIENTE.ID INNER JOIN TBMEDICAMENTO AS MEDICAMENTO 
	                 ON
	                 REQUISICAO.MEDICAMENTO_ID = MEDICAMENTO.ID

	                 WHERE
	                 REQUISICAO.ID = @ID;";

        #endregion

        public ValidationResult Inserir(Requisicao novaRequisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(novaRequisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosRequisicao(novaRequisicao, cmdInserir);
            conexao.Open();

            var id = cmdInserir.ExecuteScalar();

            novaRequisicao.Id = Convert.ToInt32(id);
            conexao.Close();
            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Id", requisicao.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }

        public Requisicao SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;

            if (leitorRequisicao.Read())
            {
                requisicao = ConverterParaRequisicao(leitorRequisicao);
            }

            conexaoComBanco.Close();

            return requisicao;
        }

        private Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["ID"]);
            int qtdMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            DateTime data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            int idFuncionario = Convert.ToInt32(leitorRequisicao["ID_FUNCIONARIO"]);
            string nomeFuncionario = Convert.ToString(leitorRequisicao["NOME_FUNCIONARIO"]);

            int idPaciente = Convert.ToInt32(leitorRequisicao["ID_PACIENTE"]);
            string nomePaciente = Convert.ToString(leitorRequisicao["NOME_PACIENTE"]);
            string cartaoSUS = Convert.ToString(leitorRequisicao["CARTAOSUS"]);

            int idMedicamento = Convert.ToInt32(leitorRequisicao["ID_MEDICAMENTO"]);
            string nomeMedicamento = Convert.ToString(leitorRequisicao["NOME_MEDICAMENTO"]);
            string lote = Convert.ToString(leitorRequisicao["LOTE"]);

            var funcionario = new Funcionario
            {
                Id = idFuncionario,
                Nome = nomeFuncionario,
            };

            var paciente = new Paciente
            {
                Id = idPaciente,
                Nome = nomePaciente,
                CartaoSUS = cartaoSUS,
            };

            var requisicao = new Requisicao
            {
                Id = id,
                QtdMedicamento = qtdMedicamento,
                Data = data,
            };

            var medicamento = new Medicamento
            {
                Id = idMedicamento,
                Nome = nomeMedicamento,
                Lote = lote,
            };

            requisicao.ConfigurarMedicamento(medicamento);
            requisicao.ConfigurarFuncionario(funcionario);
            requisicao.ConfigurarPaciente(paciente);

            return requisicao;
        }

        private static void ConfigurarParametrosRequisicao(Requisicao novaRequisicao, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("ID", novaRequisicao.Id);
            cmdInserir.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", novaRequisicao.QtdMedicamento);
            cmdInserir.Parameters.AddWithValue("DATA", novaRequisicao.Data);
            cmdInserir.Parameters.AddWithValue("FUNCIONARIO_ID", novaRequisicao.Funcionario.Id);
            cmdInserir.Parameters.AddWithValue("PACIENTE_ID", novaRequisicao.Paciente.Id);
            cmdInserir.Parameters.AddWithValue("MEDICAMENTO_ID", novaRequisicao.Medicamento.Id);
            
            
        }
    }
}
