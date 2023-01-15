using planodecontas.domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.domain.Entidades
{
    public class PlanodeConta
    {
        public int? Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CodigoCompleto { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public bool AceitaLancamento { get; set; }
        public int? IdContaPai { get; set; }
        public PlanodeConta? ContaPai { get;set; }

        public ICollection<PlanodeConta>? PlanodeContaFilhas { get; set; }

    }
}
