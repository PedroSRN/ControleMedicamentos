using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        private Funcionario funcionario;
        private RepositorioFuncionarioEmBancoDados repositorio;

        public RepositorioFuncionarioEmBancoDadosTest()
        {
            Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");

            funcionario = new Funcionario("Pedro lopes", "Pedro","0077");

            repositorio = new RepositorioFuncionarioEmBancoDados();
        }
        
        [TestMethod]
        public void Deve_inserir_Funcionario()
        {
            //Action
            repositorio.Inserir(funcionario);

            //Assert
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);

            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome); 
        }

        [TestMethod]
        public void Deve_editar_informacoes_Funcionario()
        {
            //arrange                      
            repositorio.Inserir(funcionario);

            //action
            funcionario.Nome = "João de Moraes";
            funcionario.Login = "joao";
            funcionario.Senha = "1234";

            repositorio.Editar(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            //arrange           
            repositorio.Inserir(funcionario);

            //action           
            repositorio.Excluir(funcionario);

            //assert
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);
            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_um_paciente()
        {
            //arrange          
            repositorio.Inserir(funcionario);

            //action
            var funcionarioEncontrado = repositorio.SelecionarPorNumero(funcionario.Id);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionario.Nome, funcionarioEncontrado.Nome);
        }

        [TestMethod]
        public void Deve_selecionar_todos_um_pacientes()
        {
            //arrange
            var p01 = new Funcionario("Alberto da Silva", "Alberto", "12345");
            var p02 = new Funcionario("Maria do Carmo", "Maria", "54321");
            var p03 = new Funcionario("Patricia Amorim", "Patricia", "67890");

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(p01);
            repositorio.Inserir(p02);
            repositorio.Inserir(p03);

            //action
            var funcionarios = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual(p01.Nome, funcionarios[0].Nome);
            Assert.AreEqual(p02.Nome, funcionarios[1].Nome);
            Assert.AreEqual(p03.Nome, funcionarios[2].Nome);
        }
    }
}
