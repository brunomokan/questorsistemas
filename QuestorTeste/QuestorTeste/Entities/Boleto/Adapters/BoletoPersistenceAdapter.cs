using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestorTeste.Configuration;
using QuestorTeste.Entities.Boleto.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Boleto.Adapters;

public class BoletoPersistenceAdapter : IBoletoOutputPort
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BoletoPersistenceAdapter(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BoletoModel> SaveAsync(BoletoModel model)
    {
        var entity = _mapper.Map<BoletoEntity>(model);

        await _context.Boletos.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<BoletoModel>(entity);
    }

    public async Task<BoletoModel?> FindByIdAsync(Guid id)
    {
        var entity = await _context.Boletos
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        return entity == null ? null : _mapper.Map<BoletoModel>(entity);
    }
}