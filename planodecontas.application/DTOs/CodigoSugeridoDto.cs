using planodecontas.domain.Vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.application.DTOs
{
    public class CodigoSugeridoDto
    {
        public PlanodeContaPaiVo? ContaPai { get; set; }
        public string CodigoSugerido { get; set; }
    }
}
