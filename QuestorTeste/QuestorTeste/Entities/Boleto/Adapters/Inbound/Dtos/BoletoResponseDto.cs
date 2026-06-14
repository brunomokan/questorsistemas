namespace QuestorTeste.Entities.Boleto.Adapters.Inbound.Dtos;

public record BoletoResponseDto(
    Guid Id,
    string NomeDoPagador,
    string CpfCnpjDoPagador,
    string NomeDoBeneficiario,
    string CpfCnpjDoBeneficiario,
    decimal Valor,
    DateTime DataDeVencimento,
    string? Observacao,
    Guid BancoId
);
    