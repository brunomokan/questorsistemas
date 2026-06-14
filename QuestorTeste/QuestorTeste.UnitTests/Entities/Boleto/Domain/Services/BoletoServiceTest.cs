using FluentAssertions;
using Moq;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Domain.Services;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.UnitTests.Entities.Boleto.Domain.Services
{
    public class BoletoServiceTests
    {
        private readonly Mock<IBoletoOutputPort> _boletoOutputPortMock;
        private readonly Mock<IBancoInputPort> _bancoInputPortMock;
        private readonly BoletoService _boletoService;

        public BoletoServiceTests()
        {
            _boletoOutputPortMock = new Mock<IBoletoOutputPort>();
            _bancoInputPortMock = new Mock<IBancoInputPort>();
            
            _boletoService = new BoletoService(_boletoOutputPortMock.Object, _bancoInputPortMock.Object);
        }

        [Fact]
        public async Task SaveAsync_DeveChamarOutputPortERetornarModeloSalvo()
        {
            var modelEntrada = new BoletoModel { NomeDoPagador = "Teste", Valor = 100m };
            var modelSalvo = new BoletoModel { Id = Guid.NewGuid(), NomeDoPagador = "Teste", Valor = 100m };

            _boletoOutputPortMock.Setup(p => p.SaveAsync(modelEntrada)).ReturnsAsync(modelSalvo);

            var result = await _boletoService.SaveAsync(modelEntrada);

            result.Should().BeEquivalentTo(modelSalvo);
            _boletoOutputPortMock.Verify(p => p.SaveAsync(modelEntrada), Times.Once);
        }

        [Fact]
        public async Task FindByIdAsync_QuandoBoletoNaoExiste_DeveRetornarNull()
        {
            var id = Guid.NewGuid();
            _boletoOutputPortMock.Setup(p => p.FindByIdAsync(id)).ReturnsAsync((BoletoModel?)null);

            var result = await _boletoService.FindByIdAsync(id);

            result.Should().BeNull();
            _bancoInputPortMock.Verify(p => p.FindByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task FindByIdAsync_QuandoBoletoDentroDoVencimento_NaoDeveCalcularJuros()
        {
            var id = Guid.NewGuid();
            // Data de vencimento no futuro
            var boletoNoPrazo = new BoletoModel 
            { 
                Id = id, 
                Valor = 100m, 
                DataDeVencimento = DateTime.Now.AddDays(5) 
            };

            _boletoOutputPortMock.Setup(p => p.FindByIdAsync(id)).ReturnsAsync(boletoNoPrazo);

            var result = await _boletoService.FindByIdAsync(id);

            // O valor deve continuar 100
            result!.Valor.Should().Be(100m);
            // O serviço de banco NUNCA deve ser chamado, pois não há juros a calcular
            _bancoInputPortMock.Verify(p => p.FindByIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task FindByIdAsync_QuandoBoletoVencido_DeveCalcularEAplicarJuros()
        {
            var boletoId = Guid.NewGuid();
            var bancoId = Guid.NewGuid();
            
            // Boleto vencido ontem
            var boletoVencido = new BoletoModel 
            { 
                Id = boletoId, 
                BancoId = bancoId, 
                Valor = 100m, 
                DataDeVencimento = DateTime.Now.AddDays(-1) 
            };

            // Banco com 10% de juros
            var bancoModel = new BancoModel 
            { 
                Id = bancoId, 
                PercentualDeJuros = 10m 
            };

            _boletoOutputPortMock.Setup(p => p.FindByIdAsync(boletoId)).ReturnsAsync(boletoVencido);
            _bancoInputPortMock.Setup(p => p.FindByIdAsync(bancoId)).ReturnsAsync(bancoModel);

            var result = await _boletoService.FindByIdAsync(boletoId);

            // 100 + 10% = 110
            result!.Valor.Should().Be(110m);
            
            // Verifica se o serviço de banco foi chamado para buscar o percentual
            _bancoInputPortMock.Verify(p => p.FindByIdAsync(bancoId), Times.Once);
        }
    }
}