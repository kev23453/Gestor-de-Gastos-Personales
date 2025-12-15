using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Servicios;

namespace KostoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoServicio metodoPagoServicio;
        public MetodoPagoController(IMetodoPagoServicio metodoPagoServicio)
        {
            this.metodoPagoServicio = metodoPagoServicio;
        }
        [HttpGet("obtener")]
        public IActionResult Index() { 
            return Ok(metodoPagoServicio.ObtenerMetodos());
        }

        [HttpPost("Crear")]
        public IActionResult crear(CrearMetodoPagoDTO dto)
        {
            metodoPagoServicio.crearMetodo(dto);
            return Ok("Metodo de pago creado exitosamente");
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id) { 
            metodoPagoServicio.Eliminar(id);
            return Ok("Metodo de pago Eliminado exitosamente");
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, ActualizarMetodoPagoDTO dto) {
            metodoPagoServicio.Actualizar(id, dto);
            return Ok("Metodo de pago actualizado!");
        }

    }
}
