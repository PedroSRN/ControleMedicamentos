using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class RequisicaoTest
    {
        [TestMethod]
        public void Medicamento_Da_Requisicao_Nao_Pode_Ser_Nulo()
        {
            var t = new Requisicao();
            t.Medicamento = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Medicamento' não pode ser nulo.", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Medicamento_Da_Requisicao_Nao_Pode_Ser_Vazio()
        {
            var t = new Requisicao();
            t.Medicamento = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Medicamento' não pode ser vazio.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Paciente_Da_Requisicao_Nao_Pode_Ser_Nulo()
        {
            var t = new Requisicao();
            t.Paciente = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Paciente' não pode ser nulo.", resultado.Errors[2].ErrorMessage);

        }

        [TestMethod]
        public void Paciente_Da_Requisicao_Nao_Pode_Ser_Vazio()
        {
            var t = new Requisicao();
            t.Paciente = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Paciente' não pode ser vazio.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_De_Medicamento_Da_Requisicao_Nao_Pode_Ser_Nulo()
        {
            var t = new Requisicao();
            t.QtdMedicamento = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Quantidade de Medicamento' não pode ser nula.", resultado.Errors[4].ErrorMessage);

        }

        [TestMethod]
        public void Quantidade_De_Medicamento_Da_Requisicao_Nao_Pode_Ser_Vazio()
        {
            var t = new Requisicao();
            t.QtdMedicamento = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Quantidade de Medicamento' não pode ser vazia.", resultado.Errors[5].ErrorMessage);
        }

        [TestMethod]
        public void Data_Da_Requisicao_Nao_Pode_Ser_Nula()
        {
            var t = new Requisicao();
            t.Data = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Data' não pode ser nula.", resultado.Errors[6].ErrorMessage);

        }

        [TestMethod]
        public void Data_Da_Requisicao_Nao_Pode_Ser_Vazia()
        {
            var t = new Requisicao();
            t.Data = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Data' não pode ser vazia.", resultado.Errors[7].ErrorMessage);
        }

        [TestMethod]
        public void Funcionario_Da_Requisicao_Nao_Pode_Ser_Nulo()
        {
            var t = new Requisicao();
            t.Funcionario = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Funcionario' não pode ser nulo.", resultado.Errors[8].ErrorMessage);

        }

        [TestMethod]
        public void Funcionario_Da_Requisicao_Nao_Pode_Ser_Vazio()
        {
            var t = new Requisicao();
            t.Funcionario = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Funcionario' não pode ser vazio.", resultado.Errors[9].ErrorMessage);
        }

    }
}
