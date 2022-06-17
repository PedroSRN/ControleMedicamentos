using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;


namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime? Validade { get; set; }
        public int? QuantidadeDisponivel { get; set; }

        public List<Requisicao> Requisicoes { get; set; }

        public Fornecedor Fornecedor{ get; set; }

        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public Medicamento(string nome, string descricao, string lote, DateTime validade,int qtdMedicamentos, Fornecedor fornecedor) :this()
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            QuantidadeDisponivel = qtdMedicamentos;
            Requisicoes = new List<Requisicao>();
            Fornecedor = fornecedor;
        }

        public Medicamento()
        {

        }

        public Medicamento(int v)
        {
        }

        public void ConfigurarFornecedor(Fornecedor fornecedor)
        {
            if (fornecedor == null)
                return;

            Fornecedor = fornecedor;
        }
    }
}
