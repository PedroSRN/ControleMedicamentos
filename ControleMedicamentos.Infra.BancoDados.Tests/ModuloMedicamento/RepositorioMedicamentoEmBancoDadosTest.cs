using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private Medicamento medicamento;
        private Fornecedor fornecedor;

        private RepositorioMedicamentoEmBancoDados repositorio;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;


        public RepositorioMedicamentoEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");

            fornecedor = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");

            medicamento = new Medicamento("Doril", "Para dores musculares", "1234", new(2023, 8, 9), 12, fornecedor);

            repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_Medicamento()
        {
            //Action
            repositorioFornecedor.Inserir(fornecedor);

            repositorio.Inserir(medicamento);

            //Assert
            var medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);

            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_editar_informacoes_Medicamento()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);

            repositorio.Inserir(medicamento);

            //action
            medicamento.Nome = "epocler";
            medicamento.Descricao = "cura ressaca";
            medicamento.Lote = "2333333";
            medicamento.Validade = new(2023, 8, 9);
            medicamento.QuantidadeDisponivel = 12;
            medicamento.Fornecedor = fornecedor;

            repositorio.Editar(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_excluir_Medicamento()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorio.Inserir(medicamento);

            //action           
            repositorio.Excluir(medicamento);

            //assert
            var medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);
            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_Medicamento()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorio.Inserir(medicamento);

            //action
            var medicamentoEncontrado = repositorio.SelecionarPorNumero(medicamento.Id);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamento.Nome, medicamentoEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_selecionar_todos_os_Medicamento()
        {
            //arrange
           

            var p01 = new Medicamento("Doril", "Para dores musculares", "1234", new(2023, 8, 9), 12, fornecedor);
            var p02 = new Medicamento("Eno", "Para azia", "5678", new(2024, 6, 4), 16, fornecedor);
            var p03 = new Medicamento("Xarope", "Para gripe e tosse", "0987", new(2025, 8, 9), 18, fornecedor);

            var repositorio = new RepositorioMedicamentoEmBancoDados();
            
            repositorioFornecedor.Inserir(fornecedor);

            repositorio.Inserir(p01);
            repositorio.Inserir(p02);
            repositorio.Inserir(p03);

            //action
            var medicamentos = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual(p01.Nome, medicamentos[0].Nome);
            Assert.AreEqual(p02.Nome, medicamentos[1].Nome);
            Assert.AreEqual(p03.Nome, medicamentos[2].Nome);
        }
    }
}
