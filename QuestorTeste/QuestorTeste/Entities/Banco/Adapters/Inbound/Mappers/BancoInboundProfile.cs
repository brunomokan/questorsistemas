using AutoMapper;
using QuestorTeste.Entities.Banco.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Banco.Domain.Models;

namespace QuestorTeste.Entities.Banco.Adapters.Inbound.Mappers;

public class BancoInboundProfile: Profile
{
    public BancoInboundProfile()
    {
        CreateMap<CriarBancoRequestDto, BancoModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
    }
}