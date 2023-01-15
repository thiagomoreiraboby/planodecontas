using AutoMapper;
using planodecontas.application.Contrato;
using planodecontas.application.DTOs;
using planodecontas.application.Servicos;
using planodecontas.domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace planodecontas.application.AutoMapper
{
    public class MappingProfile : Profile
    {
        private readonly IGestaodeCodigoServico gestaodeCodigoServico;

        public MappingProfile()
        {

        }
        public MappingProfile(IGestaodeCodigoServico gestaodeCodigoServico)
        {
            this.gestaodeCodigoServico = gestaodeCodigoServico;
            CreateMap<PlanodeContaDto, PlanodeConta>()
                .ForMember(c=> c.Codigo, s=> s.MapFrom(a=> gestaodeCodigoServico.GetCodigoContaByCodigoSugerido(a.Codigo)))
                .ForMember(c=> c.CodigoCompleto, s=> s.MapFrom(a=> $"{a.Codigo}"))
                .ReverseMap();
            
        }
    }
}
