using AutoMapper;
using QuestorTeste.Entities.Boleto.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Boleto.Domain.Models;

namespace QuestorTeste.Entities.Boleto.Adapters.Inbound.Mappers;

public class BoletoInboundProfile : Profile
{
    public BoletoInboundProfile()
    {
        CreateMap<CriarBoletoRequestDto, BoletoModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}