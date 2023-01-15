using planodecontas.domain.Entidades;
using planodecontas.domain.Vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.domain.Repositorios
{
    public interface IPlanodeContaRepositorio
    {
        Task<IEnumerable<PlanodeContaVo>> GetPlanodeContaByFiltro(string? descricaoConta);
        Task<PlanodeConta> GetPlanodeContaPaiById(int id);
        Task<IEnumerable<PlanodeContaPaiVo>> GetPlanodeContaPai();
        Task<bool> Inserir(PlanodeConta conta);
        Task<bool> Atualizar(PlanodeConta conta);
        Task<bool> Deletar(PlanodeConta conta);
        Task<PlanodeConta> GetPlanodeContaById(int id);
        Task<int> GetMaximoCodigoPlanodeContasPrimario();
        Task<bool> IsExixtCodigoConta(int codigo, int? idcontapai);
    }
}
