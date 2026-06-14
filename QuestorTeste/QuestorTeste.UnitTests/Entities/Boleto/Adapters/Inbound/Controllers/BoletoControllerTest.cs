using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QuestorTeste.Entities.Boleto.Adapters.Inbound.Controllers;
using QuestorTeste.Entities.Boleto.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.UnitTests.Entities.Boleto.Adapters.Inbound.Controllers
{
    public class BoletoControllerTests
    {
        private readonly Mock<IBoletoInputPort> _boletoInputPortMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BoletoController _boletoController;

        public BoletoControllerTests()
        {
            _boletoInputPortMock = new Mock<IBoletoInputPort>();
            _mapperMock = new Mock<IMapper>();
            _boletoController = new BoletoController(_boletoInputPortMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBoleto_QuandoDadosValidos_DeveRetornar200OkComDto()
        {
            var bancoId = Guid.NewGuid();
            var boletoId = Guid.NewGuid();
            var dataVencimento = DateTime.Now.AddDays(5);
            var requestDto = new CriarBoletoRequestDto("Pagador Teste", "12345678909", "Beneficiario Teste", "09876543212", 150.75m, dataVencimento, "Observacao Teste", bancoId);
            var boletoModelMapeado = new BoletoModel { NomeDoPagador = "Pagador Teste", BancoId = bancoId };
            var boletoModelSalvo = new BoletoModel { Id = boletoId, NomeDoPagador = "Pagador Teste", BancoId = bancoId };
            var responseDto = new BoletoResponseDto(boletoId, "Pagador Teste", "12345678909", "Beneficiario Teste", "09876543212", 150.75m, dataVencimento, "Observacao Teste", bancoId);

            _mapperMock.Setup(m => m.Map<BoletoModel>(requestDto)).Returns(boletoModelMapeado);
            _boletoInputPortMock.Setup(p => p.SaveAsync(boletoModelMapeado)).ReturnsAsync(boletoModelSalvo);
            _mapperMock.Setup(m => m.Map<BoletoResponseDto>(boletoModelSalvo)).Returns(responseDto);

            var result = await _boletoController.CreateBoleto(requestDto);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task CreateBoleto_QuandoRegraDeNegocioFalhar_DeveRetornar400BadRequest()
        {
            var requestDto = new CriarBoletoRequestDto("", "12345678909", "Beneficiario Teste", "09876543212", 150.75m, DateTime.Now.AddDays(5), "Observacao Teste", Guid.NewGuid());
            var boletoModelMapeado = new BoletoModel();

            _mapperMock.Setup(m => m.Map<BoletoModel>(requestDto)).Returns(boletoModelMapeado);
            _boletoInputPortMock.Setup(p => p.SaveAsync(boletoModelMapeado)).ThrowsAsync(new ArgumentException("O Nome do Pagador é obrigatório"));

            var result = await _boletoController.CreateBoleto(requestDto);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreateBoleto_QuandoRegraDeUnicidadeFalhar_DeveRetornar409Conflict()
        {
            var requestDto = new CriarBoletoRequestDto("Pagador Teste", "12345678909", "Beneficiario Teste", "09876543212", 150.75m, DateTime.Now.AddDays(5), "Observacao Teste", Guid.NewGuid());
            var boletoModelMapeado = new BoletoModel();

            _mapperMock.Setup(m => m.Map<BoletoModel>(requestDto)).Returns(boletoModelMapeado);
            _boletoInputPortMock.Setup(p => p.SaveAsync(boletoModelMapeado)).ThrowsAsync(new InvalidOperationException("Boleto já registrado com estes dados"));

            var result = await _boletoController.CreateBoleto(requestDto);

            var conflictResult = result.Should().BeOfType<ConflictObjectResult>().Subject;
            conflictResult.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task FindBoleto_QuandoEncontrarBoleto_DeveRetornar200OkComDto()
        {
            var boletoId = Guid.NewGuid();
            var bancoId = Guid.NewGuid();
            var dataVencimento = DateTime.Now.AddDays(5);
            var boletoModel = new BoletoModel { Id = boletoId, NomeDoPagador = "Pagador Teste", BancoId = bancoId };
            var responseDto = new BoletoResponseDto(boletoId, "Pagador Teste", "12345678909", "Beneficiario Teste", "09876543212", 150.75m, dataVencimento, "Observacao Teste", bancoId);

            _boletoInputPortMock.Setup(p => p.FindByIdAsync(boletoId)).ReturnsAsync(boletoModel);
            _mapperMock.Setup(m => m.Map<BoletoResponseDto>(boletoModel)).Returns(responseDto);

            var result = await _boletoController.FindBoleto(boletoId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(responseDto);
        }

        [Fact]
        public async Task FindBoleto_QuandoNaoEncontrarBoleto_DeveRetornar404NotFound()
        {
            var boletoId = Guid.NewGuid();

            _boletoInputPortMock.Setup(p => p.FindByIdAsync(boletoId)).ReturnsAsync((BoletoModel?)null);

            var result = await _boletoController.FindBoleto(boletoId);

            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.StatusCode.Should().Be(404);
            _mapperMock.Verify(m => m.Map<BoletoResponseDto>(It.IsAny<BoletoModel>()), Times.Never);
        }
    }
}