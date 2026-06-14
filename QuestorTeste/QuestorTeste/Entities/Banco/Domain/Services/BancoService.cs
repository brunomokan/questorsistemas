using AutoMapper;
using QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Banco.Domain.Services;

public class BancoService : IBancoInputPort
{
    
    private readonly IBancoOutputPort _bancoOutputPort;

    public BancoService(IBancoOutputPort bancoOutputPort)
    {
        _bancoOutputPort = bancoOutputPort;
    }

    public Task<BancoModel> SaveAsync(BancoModel model)
    {
        return _bancoOutputPort.SaveAsync(model);
    }

    public Task<IEnumerable<BancoModel>> FindAllAsync()
    {
        return  _bancoOutputPort.FindAllAsync();
    }

    public Task<BancoModel?> FindByCodeAsync(string codigo)
    {
        return  _bancoOutputPort.FindByCodeAsync(codigo);
    }
}