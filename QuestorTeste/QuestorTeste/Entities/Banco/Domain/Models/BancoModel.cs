namespace QuestorTeste.Entities.Banco.Domain.Models;

public class BancoModel
{
    public Guid Id { get; set; }
    public string NomeDoBanco { get; set; } = string.Empty;
    public string CodigoDoBanco { get; set; } = string.Empty;
    public decimal PercentualDeJuros { get; set; }
}