using AutoMapper;
using Escalas.API.Controllers.Base;
using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Escalas.API.Controllers
{
    
    [ApiController]
    [Route("api/escalas")]
    public class EscalasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IEscalaService _escalaService;

        public EscalasController(IMapper mapper, IEscalaService escalaService)
        {
            _mapper = mapper;
            _escalaService = escalaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            Log.Information("Buscando escala");

            var result = await _escalaService.GetEscalaAsync();

            Log.Information("{Count} escalas encontradas", result.Object.Count());

            return Ok(_mapper.Map<IEnumerable<Escala>, IEnumerable<EscalaModel>>(result.Object));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information("Buscando escala {Id}", id);

            var result = await _escalaService.GetEscalaByIdAsync(id);

            if (result.Notfound)
                return NotFound();

            Log.Information("Escala encontrada", result.Object);

            return Ok(_mapper.Map<Escala, EscalaModel>(result.Object));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] EscalaModel model)
        {
            var escala = _mapper.Map<EscalaModel, Escala>(model);

            if (!escala.IsValid)
                return BadRequest(escala);

            Log.Information("Cadastrando escala");

            var result = await _escalaService.CadastrarEscalaAsync(escala);

            if (!result.Success)
                return BadRequest(result);

            Log.Information("Escala inserida com sucesso");

            return Ok(new { id = result.Object });
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] EscalaModel model, int id)
        {
            var escala = _mapper.Map<EscalaModel, Escala>(model);

            if (!escala.IsValid)
                return BadRequest(escala.Notifications);

            Log.Information("Atualizando escala do dia {Data}", escala.Data);

            var result = await _escalaService.AtualizarEscalaAsync(id, escala);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("Escala do dia {Data} atualizada com sucesso" , escala.Data);

            return Ok(new { id = result.Object });
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Deletando escala {id}", id);

            var result = await _escalaService.DeletarEscalaAsync(id);

            if (result.Notfound)
                return NotFound();

            if (!result.IsValid)
                return BadRequest(result.Notifications);

            Log.Information("{id} deletado com sucesso", id);

            return Ok(new { id = result.Object });
        }
    }
}
