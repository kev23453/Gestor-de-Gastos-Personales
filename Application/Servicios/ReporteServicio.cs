using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Servicios;
using ClosedXML.Excel;

namespace Application.Servicios
{
    public class ReporteServicio : IReporteServicio
    {
        private readonly IGastoRepositorio _repo;

        public ReporteServicio(IGastoRepositorio repo)
        {
            _repo = repo;
        }

        // Método para generar reporte mensual
        public async Task<ReporteMensualDTO> GenerarReporteMensual(int año, int mes)
        {
            // Calcular mes y año anterior
            int mesAnterior = mes == 1 ? 12 : mes - 1;
            int añoAnterior = mes == 1 ? año - 1 : año;

            var gastosMes = await _repo.ObtenerGastosPorMes(año, mes);
            var gastosAnterior = await _repo.ObtenerGastosPorMes(añoAnterior, mesAnterior);

            var total = gastosMes.Sum(x => x.Monto);

            // CORRECCIÓN: Se convierte la clave (CategoriaId, que es int) a string
            var porCategoria = gastosMes
                .GroupBy(x => x.CategoriaId)
                .ToDictionary(g => g.Key.ToString(), g => g.Sum(x => x.Monto));

            // 'top' ahora contiene claves de tipo string porque 'porCategoria' tiene claves string
            var top = porCategoria
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Select(x => x.Key)
                .ToArray();

            var totalMesAnterior = gastosAnterior.Sum(x => x.Monto);

            return new ReporteMensualDTO
            {
                TotalGastado = total,
                GastosPorCategoria = porCategoria, // Diccionario con clave string
                DiferenciaVsMesAnterior = total - totalMesAnterior,
                //TopCategorias = top // Array de string
            };
        }

        // Exportar reporte a Excel
        public Task<byte[]> ExportarReporteExcel(ReporteMensualDTO reporte)
        {
            using var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Reporte");

            ws.Cell("A1").Value = "Total Gastado";
            ws.Cell("B1").Value = reporte.TotalGastado;

            ws.Cell("A3").Value = "Categoría";
            ws.Cell("B3").Value = "Monto";

            int row = 4;
            foreach (var cat in reporte.GastosPorCategoria)
            {
                // El Key es un string (el ID de la categoría convertido a string)
                ws.Cell(row, 1).Value = cat.Key;
                ws.Cell(row, 2).Value = cat.Value;
                row++;
            }

            var stream = new MemoryStream();
            wb.SaveAs(stream);

            // Importante: resetear posición para lectura externa
            stream.Position = 0;

            return Task.FromResult(stream.ToArray());
        }

        // Exportar reporte a JSON
        public Task<string> ExportarReporteJson(ReporteMensualDTO reporte)
        {
            return Task.FromResult(JsonSerializer.Serialize(reporte));
        }

        // Exportar reporte a TXT
        public Task<string> ExportarReporteTxt(ReporteMensualDTO reporte)
        {
            var txt = new StringBuilder();
            txt.AppendLine($"Total gastado: {reporte.TotalGastado}");
            txt.AppendLine($"Diferencia vs mes anterior: {reporte.DiferenciaVsMesAnterior}");
            txt.AppendLine("Gastos por categoría:");
            foreach (var cat in reporte.GastosPorCategoria)
                txt.AppendLine($" - {cat.Key}: {cat.Value}");
            txt.AppendLine("Top categorías:");
            foreach (var t in reporte.TopCategorias)
                txt.AppendLine($" - {t}");

            return Task.FromResult(txt.ToString());
        }
    }
}