using AutoMapper;
using QuestorTeste.Entities.Boleto.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Boleto.Domain.Models;

namespace QuestorTeste.Entities.Boleto.Adapters.Outbound.Mappers;

public class BoletoOutboundProfile : Profile
{
    public BoletoOutboundProfile()
    {
        CreateMap<BoletoModel, BoletoEntity>().ReverseMap();
    }
}