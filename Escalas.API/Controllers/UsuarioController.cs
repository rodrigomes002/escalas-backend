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
    [Route("api/usuario")]
    public class UsuarioController : BaseController
    { 
        private readonly IMapper _mapper;
        private readonly IUsuarioApplication _usuarioService;
        public UsuarioController(IMapper mapper, IUsuarioApplication usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UsuarioModel model)
        {
            var usuario = _mapper.Map<UsuarioModel, Usuario>(model);

            if (!usuario.IsValid)
                return BadRequest(usuario.Notifications);

            Log.Information("Cadastrando usuario {Nome}", model.Username);

            var result = await _usuarioService.CadastrarAsync(usuario);
            
            if (!result.IsValid)
                return BadRequest(result.Notifications);
            
            Log.Information("{Nome} inserido com sucesso", usuario.Username);

            return Ok(new { id = result.Object });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioModel model)
        {
            var usuario = _mapper.Map<UsuarioModel, Usuario>(model);

            if (!usuario.IsValid)
                return BadRequest(usuario.Notifications);

            Log.Information("Realizando login do usuario {Nome}", model.Username);

            var result = await _usuarioService.LoginAsync(usuario);

            if (result.Notfound)
                return NotFound();

            Log.Information("Login realizado com sucesso");

            return Ok(result);
        }
    }
}