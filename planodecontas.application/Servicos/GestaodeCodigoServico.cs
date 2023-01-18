using planodecontas.application.Contrato;
using planodecontas.application.DTOs;
using planodecontas.domain.Entidades;
using planodecontas.domain.Repositorios;
using planodecontas.domain.Vos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.application.Servicos
{
    public class GestaodeCodigoServico: IGestaodeCodigoServico
    {
        private readonly IPlanodeContaRepositorio repositorio;
        public GestaodeCodigoServico(IPlanodeContaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        private const int MaxCodigo = 999;
        public int GetCodigoContaByCodigoSugerido(string? codigoSugerido)
        {
            string? stringCodigo;

            if (string.IsNullOrEmpty(codigoSugerido))
                return 1;

            var array = codigoSugerido.Split('.');

            if (array?.Length > 0)
                stringCodigo = array[array.Length - 1];
            else
                stringCodigo = codigoSugerido;

            _ = int.TryParse(stringCodigo, out var codigo);
            return codigo;
        }

        public CodigoSugeridoDto GetCodigoContasPrimarios(int codigo)
        {
            if (DentroDoLimitedeCodigo(codigo))
            {
                return new CodigoSugeridoDto
                {
                    CodigoSugerido = $"{codigo + 1}"
                };
            }
            throw new Exception($"Não existe numero sugerido excedeu o codigo maximo: {MaxCodigo}!");
        }

        public async Task<CodigoSugeridoDto> GetCodigoSugerido(PlanodeConta entidade)
        {

            var codigofilho = await GetCodigoFilho(entidade);
            var codigoPai = await GetCodigodoPai(codigofilho.Item2);
            return new CodigoSugeridoDto
            {
                CodigoSugerido = $"{codigoPai}{codigofilho.Item1}",
                ContaPai = codigofilho.Item2 == null ? null: new PlanodeContaPaiVo(codigofilho.Item2)
            };
        }

        public void ValidarCodigoDigitado(string? codigoDigitado)
        {
            if (GetCodigoContaByCodigoSugerido(codigoDigitado) > MaxCodigo)
                throw new Exception($"Excedeu o codigo maximo: {MaxCodigo}!");
        }
        public async Task<bool> ValidarCodigoDigitadoComApí(string codigoDigitado, PlanodeConta contapai)
        {
                var codigopai = await GetCodigodoPai(contapai);
                var codigodigitadopai = string.Empty;
                var array = codigoDigitado.Split('.');
                for (int i = 0; i < array.Length - 1; i++)
                {
                    codigodigitadopai += $"{array[i]}.";
                }
            return codigopai == codigodigitadopai;
                    
        }

        private async Task<string> GetCodigodoPai(PlanodeConta? entidade, List<int> listacodigo = null)
        {
            if(entidade == null)
                return string.Empty;
            listacodigo ??= new List<int>();
            listacodigo.Insert(0, entidade.Codigo);
            if (entidade.IdContaPai != null)
            {
                var pai = await repositorio.GetPlanodeContaPaiById((int)entidade.IdContaPai);
                await GetCodigodoPai(pai, listacodigo);
            }

            var sb = new StringBuilder();
            foreach (var item in listacodigo)
            {
                sb.Append($"{item}.");
            }
            return sb.ToString();
        }

        private async Task<Tuple<int, PlanodeConta?>> GetCodigoFilho(PlanodeConta entidade)
        {
            if (entidade.PlanodeContaFilhas.Any() && !DentroDoLimitedeCodigo(entidade.PlanodeContaFilhas.Max(x => x.Codigo)))
            {
                if (entidade.IdContaPai == null)
                {
                    var ultimocodigoprimario = await repositorio.GetMaximoCodigoPlanodeContasPrimario();
                    if (DentroDoLimitedeCodigo(ultimocodigoprimario))
                        return new Tuple<int, PlanodeConta?>(ultimocodigoprimario + 1, null);
                    throw new Exception($"Excedeu o codigo maximo: {MaxCodigo}!");
                }
                var pai = await repositorio.GetPlanodeContaPaiById((int)entidade.IdContaPai);
                return await GetCodigoFilho(pai);
            }
            var codigo = entidade.PlanodeContaFilhas.Any() ? entidade.PlanodeContaFilhas.Max(x => x.Codigo) + 1 : 1;
            return new Tuple<int, PlanodeConta?>(codigo, entidade);

        }

        private bool DentroDoLimitedeCodigo(int codigo)
        {
            return codigo < MaxCodigo;
        }
    }
}
