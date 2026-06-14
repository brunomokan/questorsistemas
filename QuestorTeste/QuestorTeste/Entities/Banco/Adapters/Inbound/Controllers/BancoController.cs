using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestorTeste.Entities.Banco.Adapters.Inbound.Dtos;
using QuestorTeste.Entities.Banco.Domain.Models;
using QuestorTeste.Entities.Banco.Ports;

namespace QuestorTeste.Entities.Banco.Adapters.Inbound.Controllers
{
    
    [ApiController]
    [Route("api/v1/controller")]
    public class BancoController(IBancoInputPort bancoInputPort, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBanco([FromBody] CriarBancoRequestDto request)
        {
            try
            {
                var bancoModel = mapper.Map<BancoModel>(request);
                bancoInputPort.SaveAsync(bancoModel).Wait();
                return Ok(mapper.Map<BancoResponseDto>(bancoModel));
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

        [HttpGet]
        [Route("{codigo}")]
        public async Task<IActionResult> FindBanco([FromRoute] string codigo)
        {
            var bancoModel = await bancoInputPort.FindByCodeAsync(codigo);

            if (bancoModel == null)
            {
                return NotFound(new { Erro = $"Banco com o código '{codigo}' não foi encontrado." });
            }
            
            var responseDto = mapper.Map<BancoResponseDto>(bancoModel);
            return Ok(responseDto);
            
        }
        
        [HttpGet]
        public async Task<IActionResult> ListBancos()
        {
            var bancoModel = await bancoInputPort.FindAllAsync();
            var responseDto = mapper.Map<BancoResponseDto>(bancoModel);
            return Ok(responseDto);
        }
        
    }
}

