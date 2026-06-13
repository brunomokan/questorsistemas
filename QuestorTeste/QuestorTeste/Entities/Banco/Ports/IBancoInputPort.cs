using QuestorTeste.Entities.Banco.Domain.Models;

namespace QuestorTeste.Entities.Banco.Ports;

public interface IBancoInputPort
{
    Task<BancoModel> SaveAsync(BancoModel model);
    
    Task<IEnumerable<BancoModel>> FindAllAsync();
    
    Task<BancoModel?> FindByCodeAsync(string codigo);
}