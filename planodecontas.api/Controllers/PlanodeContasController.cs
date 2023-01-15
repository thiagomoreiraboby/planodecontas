using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using planodecontas.application.Contrato;
using planodecontas.application.DTOs;
using planodecontas.domain.Vos;

namespace planodecontas.api.Controllers
{
    [Route("api/[controller]")]
    public class PlanodeContasController : BaseController

    {
        private readonly IPlanodeContaServico servico;

        public PlanodeContasController(IPlanodeContaServico servico)
        {
            this.servico = servico;
        }

        [HttpGet(nameof(GetPlanodeContas))]
        public async Task<ActionResult<IEnumerable<PlanodeContaVo>>> GetPlanodeContas(string? descricaoConta)
        {
            var result = await servico.ListarPlanodeContas(descricaoConta);
            return ValidResult(result);
        }

        [HttpGet(nameof(GetPlanodeContasPai))]
        public async Task<ActionResult<IEnumerable<PlanodeContaPaiVo>>> GetPlanodeContasPai()
        {
            var result = await servico.ListarPlanodeContasPai();
            return ValidResult(result);
        }

        [HttpGet(nameof(GetCodigoSugeridoByIdContaPai))]
        public async Task<ActionResult<CodigoSugeridoDto>> GetCodigoSugeridoByIdContaPai(int? id)
        {
            var result = await servico.GetCodigoSugeridoByIdContaPai(id);
            return ValidResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SalvarPlanodeContas(PlanodeContaDto dto)
        {
            var result = await servico.SalvarPlanodeConta(dto);
            return ValidResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePlanodeContas(int id)
        {
            var result = await servico.DeletePlanodeContas(id);
            return ValidResult(result);
        }
    }
}
