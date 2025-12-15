using Application.Interfaces.Servicios;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KostoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteServicio _servicio;

        public ReporteController(IReporteServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("mensual")]
        public async Task<IActionResult> Mensual(int año, int mes)
        {
            var reporte = await _servicio.GenerarReporteMensual(año, mes);
            return Ok(reporte);
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportarExcel(int año, int mes)
        {
            var reporte = await _servicio.GenerarReporteMensual(año, mes);
            var excel = await _servicio.ExportarReporteExcel(reporte);
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
        }

        [HttpGet("json")]
        public async Task<IActionResult> ExportarJson(int año, int mes)
        {
            var reporte = await _servicio.GenerarReporteMensual(año, mes);
            var json = await _servicio.ExportarReporteJson(reporte);
            return Ok(json);
        }

        [HttpGet("txt")]
        public async Task<IActionResult> ExportarTxt(int año, int mes)
        {
            var reporte = await _servicio.GenerarReporteMensual(año, mes);
            var txt = await _servicio.ExportarReporteTxt(reporte);
            return File(Encoding.UTF8.GetBytes(txt), "text/plain", "reporte.txt");
        }
    }
}
