using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestorTeste.Configuration;
using QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.Entities.Banco.Adapters.Outbound;

public class BancoPersistenceAdapter(AppDbContext context, IMapper mapper) : IBancoOutputPort
{
    public async Task<BancoModel> SaveAsync(BancoModel model)
    {
        var entity = mapper.Map<BancoEntity>(model);

        await context.Bancos.AddAsync(entity);
        await context.SaveChangesAsync();

        return mapper.Map<BancoModel>(entity);
    }

    public async Task<IEnumerable<BancoModel>> FindAllAsync()
    {
        var entities = await context.Bancos
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<IEnumerable<BancoModel>>(entities);
    }

    public async Task<BancoModel?> FindByCodeAsync(string codigo)
    {
        var entity = await context.Bancos
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.CodigoDoBanco == codigo);

        return entity == null ? null : mapper.Map<BancoModel>(entity);
    }

    public async Task<BancoModel> FindByIdAsync(Guid id)
    {
        var entity = await context.Bancos
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
        
        return entity == null ? null : mapper.Map<BancoModel>(entity);
    }
}