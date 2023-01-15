using planodecontas.domain.Entidades;
using planodecontas.domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.domain.Vos
{
    public class PlanodeContaPaiVo
    {
        public PlanodeContaPaiVo()
        {

        }
        public PlanodeContaPaiVo(PlanodeConta conta)
        {
            Id = conta.Id;
            Codigo = conta.Codigo;
            Tipo = conta.Tipo;
            CodigoCompleto = conta.CodigoCompleto;
            Nome= conta.Nome;
        }

        public int? Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CodigoCompleto { get; set; }
        public TipoMovimentacao Tipo { get; set; }
    }
}
