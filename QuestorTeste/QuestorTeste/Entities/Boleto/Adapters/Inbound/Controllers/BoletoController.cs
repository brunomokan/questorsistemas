using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestorTeste.Entities.Boleto.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Boleto.Domain.Models;
using QuestorTeste.Entities.Boleto.Ports;

namespace QuestorTeste.Entities.Boleto.Adapters.Inbound.Controllers
{
    [ApiController]
    [Route("api/v1/boleto")]
    public class BoletoController(IBoletoInputPort boletoInputPort, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBoleto([FromBody] CriarBoletoRequestDto request)
        {
            try
            {
                var boletoModel = await boletoInputPort.SaveAsync(mapper.Map<BoletoModel>(request));
                return Ok(mapper.Map<BoletoResponseDto>(boletoModel));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Erro = ex.Message });
            }
        }

        [HttpGet("{boletoId}")]
        public async Task<IActionResult> FindBoleto(Guid boletoId)
        {
            var boletoModel = await boletoInputPort.FindByIdAsync(boletoId);
            
            if (boletoModel == null)
            {
                return NotFound(new {Erro = $"Boleto com o id '{boletoId}'  não foi encontrado."});
            }
            
            return Ok(mapper.Map<BoletoResponseDto>(boletoModel));
            
        }
    }
}