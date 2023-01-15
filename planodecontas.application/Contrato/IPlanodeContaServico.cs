using planodecontas.application.DTOs;
using planodecontas.application.Servicos;
using planodecontas.application.Utils;
using planodecontas.domain.Entidades;
using planodecontas.domain.Vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.application.Contrato
{
    public interface IPlanodeContaServico
    {
        Task<GenericResult> SalvarPlanodeConta(PlanodeContaDto dto);
        Task<GenericResult> ListarPlanodeContas(string? descricao);
        Task<GenericResult> ListarPlanodeContasPai();
        Task<GenericResult> GetCodigoSugeridoByIdContaPai(int? id);
        Task<GenericResult> DeletePlanodeContas(int id);
    }
}
