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
    [Route("api/usuarios")]
    public class UsuariosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUsuariosService _usuarioService;
        public UsuariosController(IMapper mapper, IUsuariosService usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioModel model)
        {
            var usuario = _mapper.Map<UsuarioModel, Usuario>(model);

            if (!usuario.IsValid)
                return BadRequest(usuario.Notifications);

            Log.Information("Realizando login do usuario {Nome}", model.Username);

            var result = await _usuarioService.LoginAsync(usuario);

            if (!result.IsValid)
                return BadRequest(result.Notifications);

            Log.Information("Login realizado com sucesso");

            return Ok(result.Object);
        }

        [Authorize(Roles = "Admin, Lider")]
        [HttpPut("add-role")]
        public async Task<IActionResult> AddRole([FromQuery] int usuarioId, [FromQuery] int cargoId)
        {
            Log.Information("Inserindo cargo a um usu�rio");

            var result = await _usuarioService.AtribuirCargoAsync(usuarioId, cargoId);

            if (result.Notfound)
                return NotFound();

            if (!result.Success)
                return BadRequest(result.Notifications);

            Log.Information("Cargo inserido com sucesso");

            return Ok(new { id = result.Object });
        }

        [AllowAnonymous]
        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string token){
            
            var result = _usuarioService.ValidateToken(token);

            return Ok(result.Object);
        }
    }
}