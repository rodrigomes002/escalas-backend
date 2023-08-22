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
        private readonly IMusicaApplication _musicaService;
        private readonly IMapper _mapper;

        public MusicaController(IMusicaApplication musicaService, IMapper mapper)
        {
            _musicaService = musicaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Log.Information("Buscando musicas");

            var result = await _musicaService.GetMusicasAsync();

            Log.Information("{Count} musicas encontradas", result.Object.Count());

            return Ok(_mapper.Map<IEnumerable<Musica>, IEnumerable<MusicaModel>>(result.Object));

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MusicaModel model)
        {
            var musica = _mapper.Map<MusicaModel, Musica>(model);

            if (!musica.IsValid)
                return BadRequest(musica.Notifications);

            Log.Information("Cadastrando musica");

            var result = await _musicaService.CadastrarMusicaAsync(musica);

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} inserida com sucesso", musica.Nome);

            return Ok(new { id = result.Object });

        }
    }
}
