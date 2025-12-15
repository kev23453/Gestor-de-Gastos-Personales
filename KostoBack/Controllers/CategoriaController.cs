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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServicio _servicio;
        private readonly ValidadorToken _validador;
        public CategoriaController(ICategoriaServicio servicio, ValidadorToken validadorToken)
        {
            _validador = validadorToken;
            _servicio = servicio;
        }
        [HttpGet("Obtener")]
        public IActionResult ObtenerTodos() {
            if(!_validador.VerificarToken())
            {
                return Unauthorized(new {message = "Usuario no autenticado"});
            }
            return Ok(_servicio.ObtenerTodos());
        }
        [HttpGet("Obtener{id}")]
        public IActionResult obtenerPorId(int id)
        {
            return Ok(_servicio.ObtenerPorID(id));
        }
        [HttpPost("agregar")]
        public IActionResult AgregarCategoria(CategoriaDTO dto)
        {
            _servicio.Crear(1, dto);
            return Ok("Categoria agregada exitosamente");
        }
        [HttpDelete]
        public IActionResult EliminarCategoria([FromBody] EliminarCategoriaDTO dto)
        {
            _servicio.Eliminar(dto);
            return Ok("Categoria eliminada exitosamente");
        }
        [HttpPut]
        public IActionResult Editar()
        {
            return Ok("hola");
        }
    }
}
