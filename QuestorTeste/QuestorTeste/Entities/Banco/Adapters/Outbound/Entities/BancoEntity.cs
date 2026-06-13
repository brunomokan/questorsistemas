namespace QuestorTeste.Entities.Banco.Adapters.Outbound.Entities;

public class BancoEntity
{
    public Guid Id { get; set; }
    public string NomeDoBanco { get; set; } = string.Empty;
    public string CodigoDoBanco { get; set; } = string.Empty;
    public decimal PercentualDeJuros { get; set; }
}