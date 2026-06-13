using QuestorTeste.Entities.Boleto.Domain.Models;

namespace QuestorTeste.Entities.Boleto.Ports;

public interface IBoletoOutputPort
{
    Task<BoletoModel> SaveAsync(BoletoModel model);
    Task<BoletoModel?> FindByIdAsync(Guid id);
}