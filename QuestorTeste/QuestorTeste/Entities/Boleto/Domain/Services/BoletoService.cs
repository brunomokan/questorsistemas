using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Boleto.Domain.Services;

public class BoletoService : IBoletoInputPort
{
    public Task<BoletoModel> SaveAsync(BoletoModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BoletoModel?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}