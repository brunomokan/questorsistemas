using System.ComponentModel.DataAnnotations;

namespace QuestorTeste.Entities.Boleto.Adapters.Inbound.Dtos;

public record CriarBoletoRequestDto(
    
    [Required(ErrorMessage = "O Nome do Pagador é obrigatório")] 
    string NomeDoPagador,
    
    [Required(ErrorMessage = "O CPF/CNPJ do Pagador é obrigatório")] 
    string CpfCnpjDoPagador,
    
    [Required(ErrorMessage = "O Nome do Beneficiário é obrigatório")] 
    string NomeDoBeneficiario,
    
    [Required(ErrorMessage = "O CPF/CNPJ do Beneficiário é obrigatório")] 
    string CpfCnpjDoBeneficiario,
    
    [Required(ErrorMessage = "O Valor é obrigatório")] 
    decimal Valor,
    
    [Required(ErrorMessage = "A Data de Vencimento é obrigatória")] 
    DateTime DataDeVencimento,
    
    string? Observacao,
    
    [Required(ErrorMessage = "O BancoId é obrigatório")] 
    Guid BancoId
);
    