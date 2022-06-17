using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void Nome_Do_Medicamento_Nao_Pode_Ser_Nulo()
        {
            var t = new Medicamento();
            t.Nome = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Nome_Do_Medicamento_Nao_Pode_Ser_Vazio()
        {
            var t = new Medicamento();
            t.Nome = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Nome' não pode ser vazio.", resultado.Errors[1].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_Do_Medicamento_Nao_Pode_Ser_Nulo()
        {
            var t = new Medicamento();
            t.Descricao = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Descricao' não pode ser nulo.", resultado.Errors[2].ErrorMessage);

        }

        [TestMethod]
        public void Descricao_Do_Medicamento_Nao_Pode_Ser_Vazio()
        {
            var t = new Medicamento();
            t.Descricao = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Descricao' não pode ser vazia.", resultado.Errors[3].ErrorMessage);
        }

        [TestMethod]
        public void Lote_Do_Medicamento_Nao_Pode_Ser_Nulo()
        {
            var t = new Medicamento();
            t.Lote = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Lote' não pode ser nulo.", resultado.Errors[4].ErrorMessage);

        }

        [TestMethod]
        public void Lote_Do_Medicamento_Nao_Pode_Ser_Vazio()
        {
            var t = new Medicamento();
            t.Lote = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Lote' não pode ser vazio.", resultado.Errors[5].ErrorMessage);
        }

        [TestMethod]
        public void Validade_Do_Medicamento_Nao_Pode_Ser_Nulo()
        {
            var t = new Medicamento();
            t.Validade = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Validade' não pode ser nula.", resultado.Errors[6].ErrorMessage);

        }

        [TestMethod]
        public void Validade_Do_Medicamento_Nao_Pode_Ser_Vazio()
        {
            var t = new Medicamento();
            t.Validade = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Validade' não pode ser vazia.", resultado.Errors[7].ErrorMessage);
        }

        [TestMethod]
        public void QuantidadeDisponivel_Do_Medicamento_Nao_Pode_Ser_Nulo()
        {
            var t = new Medicamento();
            t.QuantidadeDisponivel = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Quantidade Disponivel' não pode ser nula.", resultado.Errors[8].ErrorMessage);

        }

        [TestMethod]
        public void QuantidadeDisponivel_Do_Medicamento_Nao_Pode_Ser_Vazio()
        {
            var t = new Medicamento();
            t.QuantidadeDisponivel = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(t);

            Assert.AreEqual("'Quantidade Disponivel' não pode ser vazia.", resultado.Errors[9].ErrorMessage);
        }
    }
}
