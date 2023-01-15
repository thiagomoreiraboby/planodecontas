using Microsoft.EntityFrameworkCore;
using planodecontas.domain.Entidades;
using planodecontas.domain.Vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.infra.DBContexts
{
    public class BaseDadosContext : DbContext
    {
        public BaseDadosContext(DbContextOptions<BaseDadosContext> options) : base(options)
        {

        }
        public DbSet<PlanodeConta> PlanodeContas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanodeConta>()
                .HasOne(x => x.ContaPai)
                .WithMany(w=> w.PlanodeContaFilhas)
                .HasForeignKey(x => x.IdContaPai);
        }
    }
}
