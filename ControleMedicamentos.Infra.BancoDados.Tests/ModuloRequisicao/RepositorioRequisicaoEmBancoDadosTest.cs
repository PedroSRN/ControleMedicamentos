using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{

    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        private Requisicao requisicao;

        private Paciente paciente;
        private Funcionario funcionario;
        private Medicamento medicamento;
        private Fornecedor fornecedor;
       

        private RepositorioPacienteEmBancoDados repositorioPaciente;
        private RepositorioFornecedorEmBancoDados repositorioFornecedor;
        private RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        private RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private RepositorioRequisicaoEmBancoDados repositorio;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            //Db.ExecutarSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            Db.ExecutarSql(@"
            DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)                   
            DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)                   
            DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)                   
            DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)
            DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
           

            fornecedor = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");
            
            funcionario = new Funcionario("Eduardo", "Eduardo", "0077");

            paciente = new Paciente("Pedro lopes", "000000");

            medicamento = new Medicamento("Doril", "Para dores musculares", "1234", new(2023, 8, 9), 12, fornecedor);


            requisicao = new Requisicao(medicamento, paciente, 3, DateTime.Now, funcionario);

            repositorio = new RepositorioRequisicaoEmBancoDados();

            repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();
            repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioPaciente = new RepositorioPacienteEmBancoDados();
            
           
        }

        [TestMethod]
        public void Deve_inserir_nova_Requisicao()
        {
            //action
            repositorioFornecedor.Inserir(fornecedor);
            repositorioFuncionario.Inserir(funcionario);
            repositorioMedicamento.Inserir(medicamento);
            repositorioPaciente.Inserir(paciente);
            
            repositorio.Inserir(requisicao);

            //assert
            var requisicaoEncontrado = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrado);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrado.Id);
        }

        [TestMethod]
        public void Deve_editar_informacoes_Da_Requisicao()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioFuncionario.Inserir(funcionario);
            repositorioMedicamento.Inserir(medicamento);
            repositorioPaciente.Inserir(paciente);
            repositorio.Inserir(requisicao);

            //action
            requisicao.Medicamento.Id = 1;
            requisicao.Paciente.Id = 1;
            requisicao.QtdMedicamento = 2;
            requisicao.Data = DateTime.Now;
            requisicao.Funcionario.Id = 1;

            //assert
            var requisicaoEncontrado = repositorio.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(requisicaoEncontrado);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrado.Id);
        }

        [TestMethod]
        public void Deve_excluir_Requisicao()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioFuncionario.Inserir(funcionario);
            repositorioMedicamento.Inserir(medicamento);
            repositorioPaciente.Inserir(paciente);
            repositorio.Inserir(requisicao);

            //action           
            repositorio.Excluir(requisicao);

            //assert
            var requisicaoEncontrado = repositorio.SelecionarPorNumero(requisicao.Id);
            Assert.IsNull(requisicaoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_apenas_uma_Requisicao()
        {
            //arrange
            repositorioFornecedor.Inserir(fornecedor);
            repositorioFuncionario.Inserir(funcionario);
            repositorioMedicamento.Inserir(medicamento);
            repositorioPaciente.Inserir(paciente);
            repositorio.Inserir(requisicao);

            //action
            var requisicaoEncontrado = repositorio.SelecionarPorNumero(requisicao.Id);

            //assert
            Assert.IsNotNull(requisicaoEncontrado);
            Assert.AreEqual(requisicao.Id, requisicaoEncontrado.Id);
        }


        

        [TestMethod]
        public void Deve_selecionar_todas_As_Requisicoes()
        {
            //arrange
            Fornecedor fornecedor1 = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");
            Paciente paciente1 = new Paciente("Carlos","232323");
            Funcionario funcionario1 = new Funcionario("Pedro", "pedro", "0077");
            Medicamento medicamento1 = new Medicamento("Risotril", "Para alegrar seu dia","1234", new(2023, 8, 9), 15, fornecedor1);
            var r01 = new Requisicao(medicamento1, paciente1, 1, DateTime.Now, funcionario1);

            Fornecedor fornecedor2 = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");
            Paciente paciente2 = new Paciente("Tales","266565");
            Funcionario funcionario2 = new Funcionario("Eduardo", "Eduardo", "0078");
            Medicamento medicamento2 = new Medicamento("Estomazil","Para problemas de estômago","5678", new(2024, 9, 8), 13, fornecedor2);
            var r02 = new Requisicao(medicamento2, paciente2, 2, DateTime.Now, funcionario2);

            Fornecedor fornecedor3 = new Fornecedor("Neo Quimica", "32333040", "neoquimica@gmail.com", "São Paulo", "SP");
            Paciente paciente3 = new Paciente("Pedro","454638");
            Funcionario funcionario3 = new Funcionario("Anderson", "Anderson", "0079");
            Medicamento medicamento3 = new Medicamento("Pastilhas","Para tosse", "45454", new(2025,4,5), 20, fornecedor3);
            var r03 = new Requisicao(medicamento3, paciente3, 3, DateTime.Now, funcionario3);

            var repositorio = new RepositorioRequisicaoEmBancoDados();

            repositorioFornecedor.Inserir(fornecedor1);
            repositorioFuncionario.Inserir(funcionario1);
            repositorioPaciente.Inserir(paciente1);
            repositorioMedicamento.Inserir(medicamento1);

            repositorioFornecedor.Inserir(fornecedor2);
            repositorioFuncionario.Inserir(funcionario2);
            repositorioPaciente.Inserir(paciente2);
            repositorioMedicamento.Inserir(medicamento2);

            repositorioFornecedor.Inserir(fornecedor3);
            repositorioFuncionario.Inserir(funcionario3);
            repositorioPaciente.Inserir(paciente3);
            repositorioMedicamento.Inserir(medicamento3);
            
            repositorio.Inserir(r01);
            repositorio.Inserir(r02);
            repositorio.Inserir(r03);

            //action
            var requisicoes = repositorio.SelecionarTodos();

            //assert

            Assert.AreEqual(3, requisicoes.Count);

            Assert.AreEqual(r01.Id, requisicoes[0].Id);
            Assert.AreEqual(r02.Id, requisicoes[1].Id);
            Assert.AreEqual(r03.Id, requisicoes[2].Id);
            
        }
    }
}
