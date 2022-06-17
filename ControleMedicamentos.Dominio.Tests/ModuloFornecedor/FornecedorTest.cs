using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {
        [TestMethod]
        public void Nome_Do_Fornecedor_Nao_Pode_Ser_Nulo()
        {
            var t = new Fornecedor();
            t.Nome = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Nome_Do_Fornecedor_Nao_Pode_Ser_Vazio()
        {
            var t = new Fornecedor();
            t.Nome = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser vazio.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Do_Fornecedor_Nao_Pode_Ser_Nulo()
        {
            var t = new Fornecedor();
            t.Telefone = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Telefone' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_Do_Fornecedor_Nao_Pode_Ser_Vazio()
        {
            var t = new Fornecedor();
            t.Telefone = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Telefone' não pode ser vazio.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Email_Do_Fornecedor_Nao_Pode_Ser_Nulo()
        {
            var t = new Fornecedor();
            t.Email = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Email' não pode ser nulo.", resultado.Errors[4].ErrorMessage);
        }

        [TestMethod]
        public void Email_Do_Fornecedor_Nao_Pode_Ser_Vazio()
        {
            var t = new Fornecedor();
            t.Email = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Email' não pode ser vazio.", resultado.Errors[5].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Do_Fornecedor_Nao_Pode_Ser_Nulo()
        {
            var t = new Fornecedor();
            t.Cidade = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Cidade' não pode ser nulo.", resultado.Errors[6].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_Do_Fornecedor_Nao_Pode_Ser_Vazio()
        {
            var t = new Fornecedor();
            t.Cidade = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Cidade' não pode ser vazio.", resultado.Errors[7].ErrorMessage);
        }

        [TestMethod]
        public void Estado_Do_Fornecedor_Nao_Pode_Ser_Nulo()
        {
            var t = new Fornecedor();
            t.Estado = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Estado' não pode ser nulo.", resultado.Errors[8].ErrorMessage);
        }

        [TestMethod]
        public void Estado_Do_Fornecedor_Nao_Pode_Ser_Vazio()
        {
            var t = new Fornecedor();
            t.Estado = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Estado' não pode ser vazio.", resultado.Errors[9].ErrorMessage);
        }

    }
}
