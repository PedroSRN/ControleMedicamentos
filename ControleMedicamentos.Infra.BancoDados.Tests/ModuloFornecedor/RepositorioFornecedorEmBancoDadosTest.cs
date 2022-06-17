using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        private Fornecedor fornecedor;
        private RepositorioFornecedorEmBancoDados repositorio;

        public RepositorioFornecedorEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");

            fornecedor = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");

            repositorio = new RepositorioFornecedorEmBancoDados();
        }

        [TestMethod]
        public void Deve_inserir_Fornecedor()
        {
            //Action
            repositorio.Inserir(fornecedor);

            //Assert
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);

            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
        }


        [TestMethod]
        public void Deve_editar_informacoes_Fornecedor()
        {
            //arrange                      
            repositorio.Inserir(fornecedor);

            //action
            fornecedor.Nome = "Medley";
            fornecedor.Telefone = "987654321";
            fornecedor.Email = "medley@hotmail.com";
            fornecedor.Cidade = "Rio de Janeiro";
            fornecedor.Estado = "RJ";

            repositorio.Editar(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_excluir_Fornecedor()
        {
            //arrange           
            repositorio.Inserir(fornecedor);

            //action           
            repositorio.Excluir(fornecedor);

            //assert
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);
            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_Fornecedor()
        {
            //arrange          
            repositorio.Inserir(fornecedor);

            //action
            var fornecedorEncontrado = repositorio.SelecionarPorNumero(fornecedor.Id);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedor.Nome, fornecedorEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_selecionar_todos_um_Fornecedor()
        {
            //arrange
            var p01 = new Fornecedor("Cimed", "321654987", "cimed@gmail.com", "São paulo", "SP");
            var p02 = new Fornecedor("Prati donaduzzi", "321654987", "prati@gmail.com", "Minas gerais", "MG");
            var p03 = new Fornecedor("Eurofarma", "321654987", "eurofarma@gmail.com", "Rio de Janeiro", "RJ");

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(p01);
            repositorio.Inserir(p02);
            repositorio.Inserir(p03);

            //action
            var fornecedores = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual(p01.Nome, fornecedores[0].Nome);
            Assert.AreEqual(p02.Nome, fornecedores[1].Nome);
            Assert.AreEqual(p03.Nome, fornecedores[2].Nome);
        }

    }
}
