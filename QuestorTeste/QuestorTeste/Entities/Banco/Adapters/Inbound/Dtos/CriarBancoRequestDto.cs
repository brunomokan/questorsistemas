using System.ComponentModel.DataAnnotations;

namespace QuestorTeste.Entities.Banco.Adapters.Inbound.Dtos;

public record CriarBancoRequestDto(
    
    [Required(ErrorMessage = "O Nome do Banco é obrigatório")] 
    string NomeDoBanco,
    
    [Required(ErrorMessage = "O Código do Banco é obrigatório")] 
    string CodigoDoBanco,
    
    [Required(ErrorMessage = "O Percentual de Juros é obrigatório")] 
    decimal PercentualDeJuros
);