using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class FuncionarioTest
    {
        [TestMethod]
        public void Nome_Do_Funcionario_Nao_Pode_Ser_Nulo()
        {
            var t = new Funcionario();
            t.Nome = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_Do_Funcionario_Nao_Pode_Ser_Vazio()
        {
            var t = new Funcionario();
            t.Nome = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser vazio.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Login_Do_Funcionario_Nao_Pode_Ser_Nulo()
        {
            var t = new Funcionario();
            t.Login = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Login' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
        }

        [TestMethod]
        public void Login_Do_Funcionario_Nao_Pode_Ser_Vazio()
        {
            var t = new Funcionario();
            t.Login = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Login' não pode ser vazio.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Do_Funcionario_Nao_Pode_Ser_Nulo()
        {
            var t = new Funcionario();
            t.Senha = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Senha' não pode ser nulo.", resultado.Errors[4].ErrorMessage);
        }

        [TestMethod]
        public void Senha_Do_Funcionario_Nao_Pode_Ser_Vazio()
        {
            var t = new Funcionario();
            t.Senha = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Senha' não pode ser vazio.", resultado.Errors[5].ErrorMessage);
        }
    }
}
