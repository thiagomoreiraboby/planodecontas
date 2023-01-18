using AutoMapper;
using Microsoft.Extensions.Logging;
using planodecontas.application.Contrato;
using planodecontas.application.DTOs;
using planodecontas.application.Utils;
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
    public class PlanodeContaServico : IPlanodeContaServico
    {
        private readonly IMapper mapper;
        private readonly ILogger<PlanodeContaServico> logger;
        private readonly IPlanodeContaRepositorio repositorio;
        private readonly IGestaodeCodigoServico gestaodeCodigoServico;

        public PlanodeContaServico(
            IMapper mapper,
            ILogger<PlanodeContaServico> logger,
            IPlanodeContaRepositorio repositorio,
            IGestaodeCodigoServico gestaodeCodigoServico)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repositorio = repositorio;
            this.gestaodeCodigoServico = gestaodeCodigoServico;
        }

        public async Task<GenericResult> ListarPlanodeContas(string? descricaoConta)
        {
            var result = new GenericResult();
            try
            {
                result.Content = await repositorio.GetPlanodeContaByFiltro(descricaoConta);
            }
            catch (Exception err)
            {

                logger.ProcessException(result, $"Não foi possível listar os planos de conta: {err.Message}");
            }
            return result;
        }

        public async Task<GenericResult> GetCodigoSugeridoByIdContaPai(int? id)
        {
            var result = new GenericResult();
            try
            {
                if (id == null)
                {
                    var ultimocodigoprimario = await repositorio.GetMaximoCodigoPlanodeContasPrimario();
                    result.Content = gestaodeCodigoServico.GetCodigoContasPrimarios(ultimocodigoprimario);
                }
                else
                {
                    var entidade = await repositorio.GetPlanodeContaPaiById((int)id);
                    if(entidade == null)
                        throw new Exception("Conta pai não encontrada");
                    result.Content = await gestaodeCodigoServico.GetCodigoSugerido(entidade);
                }
               
            }
            catch (Exception err)
            {

                logger.ProcessException(result, $"Não foi possível sugerir um código: {err.Message}");
            }
            return result;
        }

        public async Task<GenericResult> SalvarPlanodeConta(PlanodeContaDto dto)
        {
            var result = new GenericResult();
            PlanodeConta? entidade;
            try
            {
                gestaodeCodigoServico.ValidarCodigoDigitado(dto.Codigo);
                
                if (dto.IdContaPai != null)
                {
                    var contapai = await repositorio.GetPlanodeContaPaiById((int)dto.IdContaPai);
                    if(contapai == null)
                        throw new Exception("Código da conta pai não foi encontrado");
                    if(! await gestaodeCodigoServico.ValidarCodigoDigitadoComApí(dto.Codigo, contapai))
                        throw new Exception($"Código digitado não pertence a conta pai informada");
                    dto.Tipo = contapai.Tipo;
                }

                entidade = mapper.Map<PlanodeConta>(dto);
                if (await repositorio.IsExixtCodigoConta(entidade.Codigo, entidade.IdContaPai))
                    throw new Exception("Código ja existe para esta conta pai");
                result.Content = await repositorio.Inserir(entidade);
            }
            catch (Exception err)
            {

                logger.ProcessException(result, $"Não foi possível salvar o plano de conta: {err.Message}");
            }
            return result;
        }

        public async Task<GenericResult> ListarPlanodeContasPai()
        {
            var result = new GenericResult();
            try
            {
                result.Content = await repositorio.GetPlanodeContaPai();
            }
            catch (Exception err)
            {

                logger.ProcessException(result, $"Não foi possível listar os planos de conta pai: {err.Message}");
            }
            return result;
        }


        public async Task<GenericResult> DeletePlanodeContas(int id)
        {
            var result = new GenericResult();
            try
            {
                var entidade = await repositorio.GetPlanodeContaById(id);
                if (entidade.PlanodeContaFilhas.Any())
                    throw new Exception("Esta conta tem contas filhas, delete as contas filhas primeiro!");
                result.Content = await repositorio.Deletar(entidade);
            }
            catch (Exception err)
            {

                logger.ProcessException(result, $"Não foi possível deletar o plano de conta: {err.Message}");
            }
            return result;
        }
    }
}
