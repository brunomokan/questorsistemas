using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Banco.Domain.Services;

public class BancoService : IBancoInputPort
{
    public Task<BancoModel> SaveAsync(BancoModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BancoModel>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BancoModel?> FindByCodeAsync(string codigo)
    {
        throw new NotImplementedException();
    }
}