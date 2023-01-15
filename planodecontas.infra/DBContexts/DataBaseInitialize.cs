using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using planodecontas.domain.Entidades;
using planodecontas.domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.infra.DBContexts
{
    public static class DataBaseInitialize
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BaseDadosContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BaseDadosContext>>()))
            {
                if (context.PlanodeContas.Any())
                {
                    return;
                }
                context.PlanodeContas.AddRange(
                    new PlanodeConta { Id = 1, Nome = "Receitas", Codigo = 1, CodigoCompleto = "1", AceitaLancamento = false, Tipo = TipoMovimentacao.Receita },
                    new PlanodeConta { Id = 2, Nome = "Taxa condominial", Codigo = 1, CodigoCompleto = "1.1", AceitaLancamento = true, Tipo = TipoMovimentacao.Receita, IdContaPai = 1 },

                    new PlanodeConta { Id = 3, Nome = "Despesas", Codigo = 2, CodigoCompleto = "2", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas },
                    new PlanodeConta { Id = 4, Nome = "Com pessoal", Codigo = 1, CodigoCompleto = "2.1", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas, IdContaPai = 3 },
                    new PlanodeConta { Id = 5, Nome = "Salário", Codigo = 1, CodigoCompleto = "2.1.1", AceitaLancamento = true, Tipo = TipoMovimentacao.Despesas, IdContaPai = 4 },
                    

                    new PlanodeConta { Id = 6, Nome = "Despesas bancárias", Codigo = 3, CodigoCompleto = "3", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas },
                    new PlanodeConta { Id = 7, Nome = "Registro de boletos", Codigo = 1, CodigoCompleto = "3.1", AceitaLancamento = true, Tipo = TipoMovimentacao.Despesas, IdContaPai = 6 },

                    new PlanodeConta { Id = 8, Nome = "Outras receitas", Codigo = 4, CodigoCompleto = "4", AceitaLancamento = false, Tipo = TipoMovimentacao.Receita },
                    new PlanodeConta { Id = 9, Nome = "Rendimento de poupança", Codigo = 1, CodigoCompleto = "4.1", AceitaLancamento = true, Tipo = TipoMovimentacao.Receita, IdContaPai = 8 },

                    new PlanodeConta { Id = 10, Nome = "Salário_2", Codigo = 9, CodigoCompleto = "9", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas },
                    new PlanodeConta { Id = 11, Nome = "Salário_2", Codigo = 9, CodigoCompleto = "9.9", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas, IdContaPai = 10 },
                    new PlanodeConta { Id = 12, Nome = "Salário_2", Codigo = 999, CodigoCompleto = "9.9.999", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas, IdContaPai = 11 },
                    new PlanodeConta { Id = 13, Nome = "Salário_2", Codigo = 999, CodigoCompleto = "9.9.999.999", AceitaLancamento = false, Tipo = TipoMovimentacao.Despesas, IdContaPai = 12 },
                    new PlanodeConta { Id = 14, Nome = "Salário_2", Codigo = 999, CodigoCompleto = "9.9.999.999.999", AceitaLancamento = true, Tipo = TipoMovimentacao.Despesas, IdContaPai = 13 },
                    new PlanodeConta { Id = 15, Nome = "Salário_2", Codigo = 10, CodigoCompleto = "9.10", AceitaLancamento = true, Tipo = TipoMovimentacao.Despesas, IdContaPai = 10 }
                    );

                context.SaveChanges();
            }
        }
    }
}
