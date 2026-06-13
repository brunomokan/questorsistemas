using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestorTeste.Configuration;
using QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.Entities.Banco.Adapters.Outbound;

public class BancoPersistenceAdapter : IBancoOutputPort
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BancoPersistenceAdapter(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BancoModel> SaveAsync(BancoModel model)
    {
        var entity = _mapper.Map<BancoEntity>(model);
        
        await _context.Bancos.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<BancoModel>(entity);
    }

    public async Task<IEnumerable<BancoModel>> FindAllAsync()
    {
        var entities = await _context.Bancos
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<BancoModel>>(entities);
    }

    public async Task<BancoModel?> FindByCodeAsync(string codigo)
    {
        var entity = await _context.Bancos
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.CodigoDoBanco == codigo);

        return entity == null ? null : _mapper.Map<BancoModel>(entity);
    }
}