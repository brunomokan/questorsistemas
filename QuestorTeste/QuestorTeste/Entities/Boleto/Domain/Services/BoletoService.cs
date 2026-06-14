using QuestorTeste.Entities.Banco.Ports;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Boleto.Domain.Services;

public class BoletoService(IBoletoOutputPort boletoOutputPort, IBancoInputPort bancoInputPort) : IBoletoInputPort
{
    public Task<BoletoModel> SaveAsync(BoletoModel model)
    {
        return boletoOutputPort.SaveAsync(model);
    }

    public async Task<BoletoModel?> FindByIdAsync(Guid id)
    {
        var boletoModel = await boletoOutputPort.FindByIdAsync(id);

        if (boletoModel == null || boletoModel.DataDeVencimento.CompareTo(DateTime.Now) >= 0) return boletoModel;
        
        var banco = await bancoInputPort.FindByIdAsync(boletoModel.BancoId);
        boletoModel.Valor += boletoModel.Valor * banco.PercentualDeJuros / 100;

        return boletoModel;
        
    }
}