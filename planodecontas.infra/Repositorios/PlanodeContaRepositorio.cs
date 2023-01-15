using Microsoft.EntityFrameworkCore;
using planodecontas.domain.Entidades;
using planodecontas.domain.Repositorios;
using planodecontas.domain.Vos;
using planodecontas.infra.DBContexts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.infra.Repositorios
{
    public class PlanodeContaRepositorio : IPlanodeContaRepositorio
    {
        private readonly BaseDadosContext context;

        public PlanodeContaRepositorio(BaseDadosContext context)
        {
            this.context = context;
        }

        public async Task<bool> Atualizar(PlanodeConta conta)
        {
            try
            {
                context.PlanodeContas.Update(conta);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Deletar(PlanodeConta conta)
        {
            try
            {
                context.PlanodeContas.Remove(conta);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PlanodeContaVo>> GetPlanodeContaByFiltro(string? descricaoConta)
        {
            return await context.PlanodeContas
                .Where(x=> (string.IsNullOrEmpty(descricaoConta) || 
                (x.CodigoCompleto.Contains(descricaoConta) || 
                x.Nome.Contains(descricaoConta) ||
                ($"{x.CodigoCompleto} {x.Nome}").StartsWith(descricaoConta)
                )))
                .OrderBy(o=> o.CodigoCompleto)
                .ThenBy(o=> o.Nome)
                .Select(s=> new PlanodeContaVo(s))
                .ToListAsync();
        }

        public async Task<PlanodeConta> GetPlanodeContaPaiById(int id)
        {
            return await context.PlanodeContas
                .Include(inc=> inc.PlanodeContaFilhas)
                .SingleOrDefaultAsync(c => c.AceitaLancamento == false && c.Id.Equals(id));
            
        }

        public async Task<PlanodeConta> GetPlanodeContaById(int id)
        {
            return await context.PlanodeContas
                .Include(inc => inc.PlanodeContaFilhas)
                .SingleOrDefaultAsync(c => c.Id.Equals(id));

        }

        public async Task<IEnumerable<PlanodeContaPaiVo>> GetPlanodeContaPai()
        {
            return await context.PlanodeContas.Where(x => x.AceitaLancamento == false).Select(s => new PlanodeContaPaiVo(s)).ToListAsync();
        }

        public async Task<int> GetMaximoCodigoPlanodeContasPrimario()
        {
            return await context.PlanodeContas?.Where(x => x.IdContaPai == null)?.MaxAsync(m=> (int?)m.Codigo) ?? 0;
        }

        public async Task<bool> Inserir(PlanodeConta conta)
        {
            try
            {
                context.PlanodeContas.Add(conta);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsExixtCodigoConta(int codigo, int? idcontapai)
        {
            return await context.PlanodeContas.AnyAsync(x => x.Codigo == codigo && x.IdContaPai == idcontapai);
        }

    }
}
