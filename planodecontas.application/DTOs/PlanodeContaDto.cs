using planodecontas.domain.Entidades;
using planodecontas.domain.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.application.DTOs
{
    public class PlanodeContaDto
    {
        public PlanodeContaDto()
        {

        }

        public PlanodeContaDto(string codigo, string nome, TipoMovimentacao tipo, bool aceitaLancamento, int? idContaPai)
        {
            Codigo = codigo;
            Nome = nome;
            Tipo = tipo;
            AceitaLancamento = aceitaLancamento;
            IdContaPai = idContaPai;
        }

        [Required(ErrorMessage = "Código deve ser preenchido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Nome deve ser preenchido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Tipo deve ser preenchido")]
        public TipoMovimentacao Tipo { get; set; }
        [Required(ErrorMessage = "Aceita lançamento deve ser preenchido")]
        public bool AceitaLancamento { get; set; }
        public int? IdContaPai { get; set; }
    }
}
