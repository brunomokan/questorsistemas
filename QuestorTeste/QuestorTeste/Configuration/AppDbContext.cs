using Microsoft.EntityFrameworkCore;
using QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;
using QuestorTeste.Entities.Boleto.Adapters.Outbound.Entities;

namespace QuestorTeste.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<BancoEntity> Bancos { get; set; }
    public DbSet<BoletoEntity> Boletos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<BancoEntity>().HasKey(b => b.Id);
        modelBuilder.Entity<BoletoEntity>().HasKey(b => b.Id);
    }
}