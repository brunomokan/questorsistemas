using FluentAssertions;
using Moq;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Domain.Services;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.UnitTests.Entities.Banco.Domain.Services
{
    public class BancoServiceTests
    {
        private readonly Mock<IBancoOutputPort> _bancoOutputPortMock;
        private readonly BancoService _bancoService;

        public BancoServiceTests()
        {
            _bancoOutputPortMock = new Mock<IBancoOutputPort>();
            _bancoService = new BancoService(_bancoOutputPortMock.Object);
        }

        [Fact]
        public async Task SaveAsync_DeveChamarOutputPortERetornarModeloSalvo()
        {
            var modelEntrada = new BancoModel { NomeDoBanco = "Banco do Brasil", CodigoDoBanco = "001", PercentualDeJuros = 1.5m };
            var modelSalvo = new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco do Brasil", CodigoDoBanco = "001", PercentualDeJuros = 1.5m };

            _bancoOutputPortMock.Setup(p => p.SaveAsync(modelEntrada)).ReturnsAsync(modelSalvo);

            var result = await _bancoService.SaveAsync(modelEntrada);

            result.Should().BeEquivalentTo(modelSalvo);
            _bancoOutputPortMock.Verify(p => p.SaveAsync(modelEntrada), Times.Once);
        }

        [Fact]
        public async Task FindAllAsync_DeveChamarOutputPortERetornarListaDeModelos()
        {
            var listaDeBancos = new List<BancoModel>
            {
                new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco 1", CodigoDoBanco = "001", PercentualDeJuros = 1.0m },
                new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco 2", CodigoDoBanco = "002", PercentualDeJuros = 2.0m }
            };

            _bancoOutputPortMock.Setup(p => p.FindAllAsync()).ReturnsAsync(listaDeBancos);

            var result = await _bancoService.FindAllAsync();

            result.Should().BeEquivalentTo(listaDeBancos);
            _bancoOutputPortMock.Verify(p => p.FindAllAsync(), Times.Once);
        }

        [Fact]
        public async Task FindByCodeAsync_QuandoBancoExiste_DeveRetornarModelo()
        {
            var codigoBacen = "001";
            var modelEsperado = new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco do Brasil", CodigoDoBanco = codigoBacen, PercentualDeJuros = 1.5m };

            _bancoOutputPortMock.Setup(p => p.FindByCodeAsync(codigoBacen)).ReturnsAsync(modelEsperado);

            var result = await _bancoService.FindByCodeAsync(codigoBacen);

            result.Should().BeEquivalentTo(modelEsperado);
            _bancoOutputPortMock.Verify(p => p.FindByCodeAsync(codigoBacen), Times.Once);
        }

        [Fact]
        public async Task FindByCodeAsync_QuandoBancoNaoExiste_DeveRetornarNull()
        {
            var codigoBacen = "999";

            _bancoOutputPortMock.Setup(p => p.FindByCodeAsync(codigoBacen)).ReturnsAsync((BancoModel?)null);

            var result = await _bancoService.FindByCodeAsync(codigoBacen);

            result.Should().BeNull();
            _bancoOutputPortMock.Verify(p => p.FindByCodeAsync(codigoBacen), Times.Once);
        }

        [Fact]
        public async Task FindByIdAsync_DeveChamarOutputPortERetornarModelo()
        {
            var id = Guid.NewGuid();
            var modelEsperado = new BancoModel { Id = id, NomeDoBanco = "Banco do Brasil", CodigoDoBanco = "001", PercentualDeJuros = 1.5m };

            _bancoOutputPortMock.Setup(p => p.FindByIdAsync(id)).ReturnsAsync(modelEsperado);

            var result = await _bancoService.FindByIdAsync(id);

            result.Should().BeEquivalentTo(modelEsperado);
            _bancoOutputPortMock.Verify(p => p.FindByIdAsync(id), Times.Once);
        }
    }
}