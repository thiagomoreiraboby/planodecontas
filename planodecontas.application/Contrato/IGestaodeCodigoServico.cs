using planodecontas.application.DTOs;
using planodecontas.domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.application.Contrato
{
    public interface IGestaodeCodigoServico
    {
        int GetCodigoContaByCodigoSugerido(string? codigoSugerido);
        CodigoSugeridoDto GetCodigoContasPrimarios(int codigo);
        Task<CodigoSugeridoDto> GetCodigoSugerido(PlanodeConta entidade);
        void ValidarCodigoDigitado(string? codigoDigitado);
        void ValidarCodigoDigitadoComApí(string codigoDigitado, PlanodeConta contapai);
    }
}
