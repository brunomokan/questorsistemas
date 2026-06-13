namespace QuestorTeste.Entities.Boleto.Domain.Models;

public class BoletoModel
{
    public Guid Id { get; set; }
    public string NomeDoPagador { get; set; } = string.Empty;
    public string CpfCnpjDoPagador { get; set; } = string.Empty;
    public string NomeDoBeneficiario { get; set; } = string.Empty;
    public string CpfCnpjDoBeneficiario { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataDeVencimento { get; set; }
    public string? Observacao { get; set; }
    public Guid BancoId { get; set; }   
}