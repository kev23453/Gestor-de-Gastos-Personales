using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Servicios
{
    public interface IReporteServicio
    {
        Task<ReporteMensualDTO> GenerarReporteMensual(int año, int mes);
        Task<byte[]> ExportarReporteExcel(ReporteMensualDTO reporte);
        Task<string> ExportarReporteJson(ReporteMensualDTO reporte);
        Task<string> ExportarReporteTxt(ReporteMensualDTO reporte);
    }
}
