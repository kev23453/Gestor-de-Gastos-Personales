using Application.DTOs;
using Application.Interfaces.Servicios;
using Infraestructura.Persistencia.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KostoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly IGastoServicio _gastoServicio;
        public GastoController(IGastoServicio gastoServicio)
        {
            _gastoServicio = gastoServicio;
        }
        [HttpGet("obtener")]
        public IActionResult ObtenerGastos()
        {
            return Ok(_gastoServicio.Listar());
        }

        [HttpPost("crear")]
        public IActionResult crear(GastoDTO dto)
        {
            _gastoServicio.Crear(dto);
            return Ok("Gasto Registrado");
        }

        [HttpGet("buscar/{id}")]
        public IActionResult obtenerPorID(int id)
        {
            return Ok(_gastoServicio.GetById(id));

        }
        [HttpDelete("{id}")]
        public IActionResult EliminarGasto(int id)
        {
            _gastoServicio.Borrar(id);
            return Ok("Gasto eliminado exitosamente");
        }
        [HttpPost("filtrar")]
        public IActionResult Filtrar([FromBody] FiltroGastosDTO filtros)
        {
            var resultado = _gastoServicio.FiltrarGastos(filtros);
            return Ok(resultado);
        }

        [HttpPost("importar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportarExcel(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            return BadRequest(new { message = "Archivo vacio" });

            var extensionesPermitidas = new[] { ".xlsx", ".xls" };
            if (!extensionesPermitidas.Contains(Path.GetExtension(archivo.FileName).ToLower()))
                return BadRequest(new { message = "Formato invalido, permitidos: xls, xlsx" });

            using var stream = archivo.OpenReadStream();
            var resultado = await _gastoServicio.ImportarExcel(stream, 1);
            return Ok(resultado);
        }

    }
}
