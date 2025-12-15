using Application.DTOs;
using Application.Interfaces.Servicios;
using Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KostoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly ValidadorToken _validadorToken;
        public UsuarioController(IUsuarioServicio usuarioServicio, ValidadorToken validadorToken)
        {
            _usuarioServicio = usuarioServicio;
            _validadorToken = validadorToken;
        }
        [HttpPost("perfil/editar")]
        public IActionResult cambiarContraseña(ActualizarPerfilDTO dto) 
        {
            _usuarioServicio.Perfil(dto, _validadorToken.TokenID());
            return Ok("Perfil Modificado");
        }
    }
}
