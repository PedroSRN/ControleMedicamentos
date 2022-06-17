using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class PacienteTest
    {
        [TestMethod]
        public void Nome_Do_Paciente_Nao_Pode_Ser_Nulo()
        {
            var t = new Paciente();
            t.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
          
        }

        [TestMethod]
        public void Nome_Do_Paciente_Nao_Pode_Ser_Vazio()
        {
            var t = new Paciente();
            t.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser vazio.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Do_Paciente_Nao_Pode_Ser_Nulo()
        {
            var t = new Paciente();
            t.CartaoSUS = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Cartão SUS' não pode ser nulo.", resultado.Errors[2].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_Do_Paciente_Nao_Pode_Ser_Vazio()
        {
            var t = new Paciente();
            t.CartaoSUS = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Cartão SUS' não pode ser vazio.", resultado.Errors[3].ErrorMessage);
        }

    }
}
