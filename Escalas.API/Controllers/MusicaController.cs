using AutoMapper;
using Escalas.API.Controllers.Base;
using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Escalas.API.Controllers
{
    [ApiController]
    [Route("api/musica")]
    public class MusicaController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMusicaApplication _musicaService;

        public MusicaController(IMapper mapper, IMusicaApplication musicaService)
        {
            _mapper = mapper;
            _musicaService = musicaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Log.Information("Buscando musicas");

            var result = await _musicaService.GetMusicasAsync();

            Log.Information("{Count} musicas encontradas", result.Object.Count());

            return Ok(_mapper.Map<IEnumerable<Musica>, IEnumerable<MusicaModel>>(result.Object));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information("Buscando musica {id}", id);

            var result = await _musicaService.GetMusicaByIdAsync(id);

            if (result.Notfound)
                return NotFound();

            Log.Information("Musica encontrada", result.Object);

            return Ok(_mapper.Map<Musica, MusicaModel>(result.Object));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MusicaModel model)
        {
            var musica = _mapper.Map<MusicaModel, Musica>(model);

            if (!musica.IsValid)
                return BadRequest(musica.Notifications);

            Log.Information("Cadastrando musica {Nome}", model.Nome);

            var result = await _musicaService.CadastrarMusicaAsync(musica);

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} inserida com sucesso", musica.Nome);

            return Ok(new { id = result.Object });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] MusicaModel model, int id)
        {
            var musica = _mapper.Map<MusicaModel, Musica>(model);

            if (!musica.IsValid)
                return BadRequest(musica.Notifications);

            Log.Information("Atualizando musica {Nome}", musica.Nome);

            var result = await _musicaService.AtualizarMusicaAsync(id, musica);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} atualizada com sucesso", musica.Nome);

            return Ok(new { id = result.Object });
        }
    }
}