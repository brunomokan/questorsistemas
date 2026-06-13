using QuestorTeste.Entities.Boleto.Domain.Models;

namespace QuestorTeste.Entities.Boleto.Ports;

public interface IBoletoInputPort
{
    Task<BoletoModel> SaveAsync(BoletoModel model);
    
    Task<BoletoModel?> FindByIdAsync(Guid id);
}