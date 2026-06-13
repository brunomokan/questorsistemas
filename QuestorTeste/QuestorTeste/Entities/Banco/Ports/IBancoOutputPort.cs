using QuestorTeste.Entities.Banco.Domain.Models;

namespace QuestorTeste.Entities.Banco.Ports;

public interface IBancoOutputPort
{
    Task<BancoModel> SaveAsync(BancoModel model);
    Task<IEnumerable<BancoModel>> FindAllAsync();
    Task<BancoModel?> FindByCodeAsync(string codigo);
}