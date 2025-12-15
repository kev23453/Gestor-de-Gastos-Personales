using Application.DTOs;
using Application.Servicios;
using Infraestructura.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Servicios;

namespace KostoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GeneradorJwt _jwt;
        private readonly IUsuarioServicio _usuarioServicio;
        public AuthController(GeneradorJwt jwt, IUsuarioServicio usuarioServicio)
        {
            _jwt = jwt;
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            var Usuario = _usuarioServicio.Login(dto);
            var token = _jwt.GenerateJwt(Usuario);
            return Ok(token);
        }

        [HttpPost("Ingresar")]
        public IActionResult Ingresar(RegisterDTO dto)
        {
            _usuarioServicio.Registrar(dto);
            return Ok("Usuario Registrado");
        }

    }
}
