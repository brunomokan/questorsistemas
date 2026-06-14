using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.Entities.Banco.Domain.Services;

public class BancoService(IBancoOutputPort bancoOutputPort) : IBancoInputPort
{
    public Task<BancoModel> SaveAsync(BancoModel model)
    {
        return bancoOutputPort.SaveAsync(model);
    }

    public Task<IEnumerable<BancoModel>> FindAllAsync()
    {
        return bancoOutputPort.FindAllAsync();
    }

    public Task<BancoModel?> FindByCodeAsync(string codigo)
    {
        return bancoOutputPort.FindByCodeAsync(codigo);
    }

    public Task<BancoModel> FindByIdAsync(Guid id)
    {
        return bancoOutputPort.FindByIdAsync(id);
    }
}