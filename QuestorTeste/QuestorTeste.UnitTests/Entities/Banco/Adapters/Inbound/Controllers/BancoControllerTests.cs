using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuestorTeste.Entities.Banco.Adapters.Inbound.Controllers;
using QuestorTeste.Entities.Banco.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.UnitTests.Entities.Banco.Adapters.Inbound.Controllers
{
    public class BancoControllerTests
    {
        private readonly Mock<IBancoInputPort> _bancoInputPortMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BancoController _bancoController;

        public BancoControllerTests()
        {
            _bancoInputPortMock = new Mock<IBancoInputPort>();
            _mapperMock = new Mock<IMapper>();
            _bancoController = new BancoController(_bancoInputPortMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBanco_QuandoDadosValidos_DeveRetornar200OkComDto()
        {
            var requestDto = new CriarBancoRequestDto("Banco do Brasil", "001", 1.5m);
            var bancoModelMapeado = new BancoModel { NomeDoBanco = "Banco do Brasil", CodigoDoBanco = "001", PercentualDeJuros = 1.5m };
            var bancoModelSalvo = new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco do Brasil", CodigoDoBanco = "001", PercentualDeJuros = 1.5m };
            var responseDto = new BancoResponseDto(bancoModelSalvo.Id, "Banco do Brasil", "001", 1.5m);

            _mapperMock.Setup(m => m.Map<BancoModel>(requestDto)).Returns(bancoModelMapeado);
            _bancoInputPortMock.Setup(p => p.SaveAsync(bancoModelMapeado)).ReturnsAsync(bancoModelSalvo);
            _mapperMock.Setup(m => m.Map<BancoResponseDto>(bancoModelSalvo)).Returns(responseDto);

            var result = await _bancoController.CreateBanco(requestDto);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task CreateBanco_QuandoRegraDeNegocioFalhar_DeveRetornar400BadRequest()
        {
            var requestDto = new CriarBancoRequestDto("", "001", 1.5m);
            var bancoModelMapeado = new BancoModel();
            
            _mapperMock.Setup(m => m.Map<BancoModel>(requestDto)).Returns(bancoModelMapeado);
            _bancoInputPortMock.Setup(p => p.SaveAsync(bancoModelMapeado)).ThrowsAsync(new ArgumentException("O Nome do Banco é obrigatório"));

            var result = await _bancoController.CreateBanco(requestDto);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreateBanco_QuandoBancoJaExistir_DeveRetornar409Conflict()
        {
            var requestDto = new CriarBancoRequestDto("Banco do Brasil", "001", 1.5m);
            var bancoModelMapeado = new BancoModel();
            
            _mapperMock.Setup(m => m.Map<BancoModel>(requestDto)).Returns(bancoModelMapeado);
            _bancoInputPortMock.Setup(p => p.SaveAsync(bancoModelMapeado)).ThrowsAsync(new InvalidOperationException("Já existe um banco com este código"));

            var result = await _bancoController.CreateBanco(requestDto);

            var conflictResult = result.Should().BeOfType<ConflictObjectResult>().Subject;
            conflictResult.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task FindBanco_QuandoEncontrarBanco_DeveRetornar200OkComDto()
        {
            var codigoBacen = "001";
            var bancoModel = new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco do Brasil", CodigoDoBanco = codigoBacen, PercentualDeJuros = 1.5m };
            var responseDto = new BancoResponseDto(bancoModel.Id, "Banco do Brasil", codigoBacen, 1.5m);

            _bancoInputPortMock.Setup(p => p.FindByCodeAsync(codigoBacen)).ReturnsAsync(bancoModel);
            _mapperMock.Setup(m => m.Map<BancoResponseDto>(bancoModel)).Returns(responseDto);

            var result = await _bancoController.FindBanco(codigoBacen);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task FindBanco_QuandoNaoEncontrarBanco_DeveRetornar404NotFound()
        {
            var codigoBacen = "999";
            
            _bancoInputPortMock.Setup(p => p.FindByCodeAsync(codigoBacen)).ReturnsAsync((BancoModel?)null);

            var result = await _bancoController.FindBanco(codigoBacen);

            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.StatusCode.Should().Be(404);
            _mapperMock.Verify(m => m.Map<BancoResponseDto>(It.IsAny<BancoModel>()), Times.Never);
        }

        [Fact]
        public async Task ListBancos_DeveRetornar200OkComListaDeDtos()
        {
            var bancosModels = new List<BancoModel>
            {
                new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco 1", CodigoDoBanco = "001", PercentualDeJuros = 1.5m },
                new BancoModel { Id = Guid.NewGuid(), NomeDoBanco = "Banco 2", CodigoDoBanco = "002", PercentualDeJuros = 2.0m }
            };

            var responseDtos = new List<BancoResponseDto>
            {
                new BancoResponseDto(bancosModels[0].Id, "Banco 1", "001", 1.5m),
                new BancoResponseDto(bancosModels[1].Id, "Banco 2", "002", 2.0m)
            };

            _bancoInputPortMock.Setup(p => p.FindAllAsync()).ReturnsAsync(bancosModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<BancoResponseDto>>(bancosModels)).Returns(responseDtos);

            var result = await _bancoController.ListBancos();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(responseDtos);
        }
    }
}