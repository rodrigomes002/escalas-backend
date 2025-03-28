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
    [Route("api/musicos")]
    public class MusicosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMusicosService _musicoApplication;

        public MusicosController(IMapper mapper, IMusicosService musicoApplication)
        {
            _mapper = mapper;
            _musicoApplication = musicoApplication;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? nome)
        {
            Log.Information("Buscando Musicos");

            var result = await _musicoApplication.GetMusicosAsync(pageNumber, pageSize, nome);

            Log.Information("{Count} Musicos encontradas", result.Object.TotalCount);

            return Ok(result.Object);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information("Buscando Musico {id}", id);

            var result = await _musicoApplication.GetMusicoByIdAsync(id);

            if (result.Notfound)
                return NotFound();

            Log.Information("Musico encontrado", result.Object);

            return Ok(_mapper.Map<Musico, MusicoModel>(result.Object));
        }

        [Authorize(Roles = "Admin, Lider")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MusicoModel model)
        {
            var Musico = _mapper.Map<MusicoModel, Musico>(model);

            if (!Musico.IsValid)
                return BadRequest(Musico.Notifications);

            Log.Information("Cadastrando Musico {Nome}", model.Nome);

            var result = await _musicoApplication.CadastrarMusicoAsync(Musico);

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} inserida com sucesso", Musico.Nome);

            return Ok(new { id = result.Object });
        }

        [Authorize(Roles = "Admin, Lider")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] MusicoModel model, int id)
        {
            var Musico = _mapper.Map<MusicoModel, Musico>(model);

            if (!Musico.IsValid)
                return BadRequest(Musico.Notifications);

            Log.Information("Atualizando Musico {Nome}", Musico.Nome);

            var result = await _musicoApplication.AtualizarMusicoAsync(id, Musico);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{Nome} atualizada com sucesso", Musico.Nome);

            return Ok(new { id = result.Object });
        }

        [Authorize(Roles = "Admin, Lider")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Deletando Musico {id}", id);

            var result = await _musicoApplication.DeletarMusicoAsync(id);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("{id} deletado com sucesso", id);

            return Ok(new { id = result.Object });
        }
    }
}