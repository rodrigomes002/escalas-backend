using Escalas.Application;
using Escalas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Escalas.API.Controllers
{
    [ApiController]
    [Route("api/musicas")]
    public class MusicaController : Controller
    {
        private readonly IMusicaService _musicaService;

        public MusicaController(IMusicaService musicaService)
        {
            _musicaService = musicaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Log.Information("Buscando musicas");
            return Ok(await _musicaService.GetMusicasAsync());

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MusicaRequest request)
        {
            Log.Information("Cadastrando musica");
            await _musicaService.CadastrarMusicaAsync(request);
            return Ok();

        }
    }
}
