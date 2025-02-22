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
    [Authorize]
    [ApiController]
    [Route("api/cargos")]
    public class CargosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICargoService _cargoService;

        public CargosController(IMapper mapper, ICargoService cargoService)
        {
            _mapper = mapper;
            _cargoService = cargoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? nome)
        {
            Log.Information("Buscando cargos");

            var result = await _cargoService.GetCargosAsync(pageNumber, pageSize, nome);

            Log.Information("{Count} Cargos encontrados", result.Object.TotalCount);

            return Ok(result.Object);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information("Buscando cargo {id}", id);

            var result = await _cargoService.GetCargosByIdAsync(id);

            Log.Information("Cargo encontrado", result.Object);

            return Ok(_mapper.Map<Cargo, CargoModel>(result.Object));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoModel model)
        {
            var cargo = _mapper.Map<CargoModel, Cargo>(model);

            if (!cargo.IsValid)
                return BadRequest(cargo.Notifications);

            Log.Information("Cadastrando cargo {Nome}", cargo.Nome);

            var result = await _cargoService.CadastrarCargoAsync(cargo);

            Log.Information("{Nome} inserido com sucesso", cargo.Nome);

            return Ok(new { id = result.Object });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] CargoModel model, int id)
        {
            var cargo = _mapper.Map<CargoModel, Cargo>(model);

            if (!cargo.IsValid)
                return BadRequest(cargo.Notifications);

            Log.Information("Atualizando cargo {Nome}", cargo.Nome);

            var result = await _cargoService.AtualizarCargoAsync(id, cargo);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} atualizado com sucesso", cargo.Nome);

            return Ok(new { id = result.Object });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Deletando cargo {id}", id);

            var result = await _cargoService.DeletarCargoAsync(id);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{id} deletado com sucesso", id);

            return Ok(new { id = result.Object });
        }
    }
}
