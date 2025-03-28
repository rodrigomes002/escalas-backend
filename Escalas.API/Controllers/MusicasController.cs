﻿using AutoMapper;
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
    [Route("api/musicas")]
    public class MusicasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMusicasService _musicaService;
        private readonly IMusicosService _musicoService;

        public MusicasController(IMapper mapper, IMusicasService musicaService, IMusicosService musicoService)
        {
            _mapper = mapper;
            _musicaService = musicaService;
            _musicoService = musicoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? nome)
        {
            Log.Information("Buscando musicas");

            var result = await _musicaService.GetMusicasAsync(pageNumber, pageSize, nome);

            Log.Information("{Count} musicas encontradas", result.Object.TotalCount);

            return Ok(result.Object);
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

        [Authorize(Roles = "Admin, Lider")]
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

        [Authorize(Roles = "Admin, Lider")]
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

        [Authorize(Roles = "Admin, Lider")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Deletando musica {id}", id);

            var result = await _musicaService.DeletarMusicaAsync(id);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{id} deletado com sucesso", id);

            return Ok(new { id = result.Object });
        }
    }
}