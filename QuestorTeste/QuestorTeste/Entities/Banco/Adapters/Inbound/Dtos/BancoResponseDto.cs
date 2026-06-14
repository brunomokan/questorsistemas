using System.ComponentModel.DataAnnotations;

namespace QuestorTeste.Entities.Banco.Adapters.Inbound.Dtos;

public record BancoResponseDto(
    Guid Id,
    string NomeDoBanco,
    string CodigoDoBanco,
    decimal PercentualDeJuros
);