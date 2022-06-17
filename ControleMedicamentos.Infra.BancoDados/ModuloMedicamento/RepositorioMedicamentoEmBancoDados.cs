using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados
    {
        private const string enderecoBanco =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ControleMedicamentosDB;Integrated Security=True;
            Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO[TBMEDICAMENTO]
            (
               [NOME],
               [DESCRICAO],
               [LOTE],
               [VALIDADE],
               [QUANTIDADEDISPONIVEL],
               [FORNECEDOR_ID]
            )
            VALUES
            (
                @NOME,
                @DESCRICAO,
                @LOTE,
                @VALIDADE,
                @QUANTIDADEDISPONIVEL,
                @FORNECEDOR_ID
            );SELECT SCOPE_IDENTITY();";



        private const string sqlEditar =
            @"UPDATE [TBMEDICAMENTO]
            SET 
                [NOME] = @NOME,
                [DESCRICAO] = @DESCRICAO,
                [LOTE] = @LOTE,
                [VALIDADE] = @VALIDADE,
                [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL, 
                [FORNECEDOR_ID] = @FORNECEDOR_ID 
            WHERE
	             [ID] = @ID;";


        private const string sqlExcluir = 
            @"DELETE FROM[TBMEDICAMENTO]
                WHERE
	                  [ID] = @ID;";

        private const string sqlSelecionarTodos =
            @"SELECT 
                    MEDICAMENTO.ID,
                    MEDICAMENTO.NOME,
                    MEDICAMENTO.DESCRICAO,
                    MEDICAMENTO.LOTE,
                    MEDICAMENTO.VALIDADE,
                    MEDICAMENTO.QUANTIDADEDISPONIVEL,
       
		            FORNECEDOR.ID AS FORNECEDOR_NUMERO,
		            FORNECEDOR.NOME AS FORNECEDOR_NOME

              FROM
                   TBMEDICAMENTO AS MEDICAMENTO INNER JOIN TBFORNECEDOR AS FORNECEDOR
              ON
                   MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID;";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                    MEDICAMENTO.ID,
                    MEDICAMENTO.NOME,
                    MEDICAMENTO.DESCRICAO,
                    MEDICAMENTO.LOTE,
                    MEDICAMENTO.VALIDADE,
                    MEDICAMENTO.QUANTIDADEDISPONIVEL,
       
		            FORNECEDOR.ID AS FORNECEDOR_NUMERO,
		            FORNECEDOR.NOME AS FORNECEDOR_NOME

              FROM
                   TBMEDICAMENTO AS MEDICAMENTO INNER JOIN TBFORNECEDOR AS FORNECEDOR
              ON
                   MEDICAMENTO.FORNECEDOR_ID = FORNECEDOR.ID 

             WHERE
                  MEDICAMENTO.ID = @ID";

        #endregion


        public ValidationResult Inserir(Medicamento novoMedicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(novoMedicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosMedicamento(novoMedicamento, cmdInserir);
            conexao.Open();

            var id = cmdInserir.ExecuteScalar();

            novoMedicamento.Id = Convert.ToInt32(id);
            conexao.Close();
            return resultadoValidacao;
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Id", medicamento.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;
        }

        public Medicamento SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;

            if (leitorMedicamento.Read())
            {
                medicamento = ConverterParaMedicamento(leitorMedicamento);
            }

            conexaoComBanco.Close();

            return medicamento;
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            int id = Convert.ToInt32(leitorMedicamento["ID"]);
            string nome = Convert.ToString(leitorMedicamento["NOME"]);
            string descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            string lote = Convert.ToString(leitorMedicamento["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);
            
            int idFornecedor = Convert.ToInt32(leitorMedicamento["FORNECEDOR_NUMERO"]);
            string nomeFornecedor = Convert.ToString(leitorMedicamento["FORNECEDOR_NOME"]);

            var fornecedor = new Fornecedor
            {
                Id = idFornecedor,
                Nome = nomeFornecedor,
            };


            var medicamento = new Medicamento
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = quantidadeDisponivel,
                ///Requisicoes = requisicoes,

            };

            medicamento.ConfigurarFornecedor(fornecedor);

            return medicamento;
        }
        private static void ConfigurarParametrosMedicamento(Medicamento novoMedicamento, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("ID", novoMedicamento.Id);
            cmdInserir.Parameters.AddWithValue("NOME", novoMedicamento.Nome);
            cmdInserir.Parameters.AddWithValue("DESCRICAO", novoMedicamento.Descricao);
            cmdInserir.Parameters.AddWithValue("LOTE", novoMedicamento.Lote);
            cmdInserir.Parameters.AddWithValue("VALIDADE", novoMedicamento.Validade);
            cmdInserir.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", novoMedicamento.QuantidadeDisponivel);
            cmdInserir.Parameters.AddWithValue("FORNECEDOR_ID", novoMedicamento.Fornecedor.Id);
            
            
        }
    }
}
    