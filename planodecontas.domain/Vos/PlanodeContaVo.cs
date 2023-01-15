using planodecontas.domain.Entidades;
using planodecontas.domain.Enuns;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.domain.Vos
{
    public class PlanodeContaVo
    {
        public PlanodeContaVo()
        {

        }
        public PlanodeContaVo(PlanodeConta conta)
        {
            Id = conta.Id;
            Codigo = conta.Codigo;
            Tipo = conta.Tipo;
            Nome = conta.Nome;
            CodigoCompleto = conta.CodigoCompleto;
            AceitaLancamento = conta.AceitaLancamento;
            IdContaPai = conta.IdContaPai;
        }

        public int? Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CodigoCompleto { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public bool AceitaLancamento { get; set; }
        public int? IdContaPai { get; set; }
    }
}
