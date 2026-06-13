using AutoMapper;
using QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Banco.Domain.Models;

namespace QuestorTeste.Entities.Banco.Adapters.Outbound.Mappers;

public class BancoOutboundProfile : Profile
{
    public BancoOutboundProfile()
    {
        CreateMap<BancoModel, BancoEntity>().ReverseMap();
    }
}