using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Excepciones;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Servicios;
using Dominio.Entidades;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Application.Servicios
{
    public class GastoServicio : IGastoServicio
    {
        private readonly IGastoRepositorio _gastoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IMetodoPagoRepositorio _metodoPagoRepositorio;
        public GastoServicio(IGastoRepositorio gastoRepositorio, ICategoriaRepositorio categoriaRepositorio, IMetodoPagoRepositorio metodoPagoRepositorio)
        {
            _metodoPagoRepositorio = metodoPagoRepositorio;
            _gastoRepositorio = gastoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
        }

        public void Borrar(int id)
        {
            var busquedaGasto = this.GetById(id);
            if (busquedaGasto == null)
            {
                throw new BusinessException($"No se encuentra un gasto con el ID: {id}");
            }
            _gastoRepositorio.eliminar(id);
        }

        public void Crear(GastoDTO gasto)
        {
            var NuevoGasto = new Gasto(
                gasto.Monto,
                gasto.Fecha,
                gasto.CategoriaId,
                gasto.MetodoPagoId,
                gasto.Descripcion,
                gasto.UsuarioId
            );
            _gastoRepositorio.insertar( NuevoGasto );
        }

        public List<GastoDTO> FiltrarGastos(FiltroGastosDTO filtros)
        {
            var query = _gastoRepositorio.Query();
            if (filtros.FechaInicio.HasValue)
                query = query.Where(gasto => gasto.Fecha >= filtros.FechaInicio.Value);
            if (filtros.FechaFin.HasValue)
                query = query.Where(gasto => gasto.Fecha <= filtros.FechaFin.Value);
            if (filtros.CategoriaId.HasValue)
                query = query.Where(gasto => gasto.CategoriaId == filtros.CategoriaId.Value);
            if (filtros.MetodoPagoId.HasValue)
                query = query.Where(gasto => gasto.MetodoPagoId == filtros.MetodoPagoId.Value);
            if (!string.IsNullOrWhiteSpace(filtros.Descripcion))
                query = query.Where(g => g.Descripcion.Contains(filtros.Descripcion));

            query = query.OrderByDescending(g => g.Fecha);

            return query.Select(gasto => new GastoDTO
            {
                Monto = gasto.Monto,
                Fecha = gasto.Fecha,
                CategoriaId = gasto.CategoriaId,
                MetodoPagoId = gasto.MetodoPagoId,
                Descripcion = gasto.Descripcion
            }).ToList();
        }

        public GastoDTO GastoXcategoria(int idCategoria)
        {
            var gastos = _gastoRepositorio.GastoXcategoria(idCategoria); 
            if(gastos == null)
            {
                throw new BusinessException("No hay gastos asociados a esta categoria");
            }
            return new GastoDTO
            {
                Monto = gastos.Monto,
                Fecha = gastos.Fecha,
                CategoriaId = gastos.CategoriaId,
                MetodoPagoId = gastos.MetodoPagoId,
                Descripcion = gastos.Descripcion,
                UsuarioId = gastos.UsuarioId
            };
        }

        public GastoDTO GetById(int id)
        {
            var gasto = _gastoRepositorio.get(id);
            if(gasto == null)
            {
                throw new BusinessException($"No se encontraron gastos con el id: {id}");
            }
            return new GastoDTO
            {
                Monto = gasto.Monto,
                Fecha = gasto.Fecha,
                CategoriaId = gasto.CategoriaId,
                MetodoPagoId = gasto.MetodoPagoId,
                Descripcion = gasto.Descripcion,
                UsuarioId = gasto.UsuarioId
            };
        }

        public Task<ResultadoImportacionDTO> ImportarExcel(Stream stream, int usuarioId)
        {
            var resultado = new ResultadoImportacionDTO();

            using (var workbook = new XLWorkbook(stream))
            {
                var sheet = workbook.Worksheet(1);
                var lastRow = sheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRow; row++)
                {
                    try
                    {
                        // --- DESCRIPCIÓN ---
                        var descripcion = sheet.Cell(row, 1).GetString();

                        // --- MONTO (limpiar $ y convertir) ---
                        var montoTexto = sheet.Cell(row, 2).GetString()
                            .Replace("$", "")
                            .Trim();

                        if (!decimal.TryParse(montoTexto, out decimal monto))
                        {
                            resultado.Errores.Add($"Fila {row}: Monto inválido '{montoTexto}'");
                            continue;
                        }

                        if (monto <= 0)
                        {
                            resultado.Errores.Add($"Fila {row}: El monto debe ser mayor a 0.");
                            continue;
                        }

                        // --- FECHA ---
                        var fechaTexto = sheet.Cell(row, 3).GetString();

                        if (!DateTime.TryParse(fechaTexto, out DateTime fecha))
                        {
                            resultado.Errores.Add($"Fila {row}: Fecha inválida '{fechaTexto}'");
                            continue;
                        }

                        // --- CAT Y METODO ---
                        var categoriaNombre = sheet.Cell(row, 4).GetString();
                        var metodoPagoNombre = sheet.Cell(row, 5).GetString();

                        var busquedaCategoria = _categoriaRepositorio.GetCategoriaByName(categoriaNombre);
                        var busquedaMetodo = _metodoPagoRepositorio.GetByName(metodoPagoNombre);

                        if (busquedaCategoria == null)
                        {
                            resultado.Errores.Add($"Fila {row}: Categoría inválida '{categoriaNombre}'.");
                            continue;
                        }

                        if (busquedaMetodo == null)
                        {
                            resultado.Errores.Add($"Fila {row}: Método de pago inválido '{metodoPagoNombre}'.");
                            continue;
                        }

                        // --- CREAR GASTO ---
                        var gasto = new Gasto
                        {
                            Descripcion = descripcion,
                            Monto = monto,
                            Fecha = fecha,
                            CategoriaId = busquedaCategoria.Id,
                            MetodoPagoId = busquedaMetodo.Id,
                            UsuarioId = usuarioId
                        };

                        _gastoRepositorio.insertar(gasto);
                        resultado.RegistrosExitosos++;
                    }
                    catch (BusinessException error)
                    {
                        resultado.Errores.Add($"Fila {row}: Error: - {error.Message}");
                    }
                }
            }

            return Task.FromResult(resultado);
        }


        public IEnumerable<GastoDTO> Listar()
        {
            return _gastoRepositorio.GetAll().Select(gasto => new GastoDTO
            {
                Monto = gasto.Monto,
                Fecha = gasto.Fecha,
                CategoriaId = gasto.CategoriaId,
                MetodoPagoId = gasto.MetodoPagoId,
                Descripcion = gasto.Descripcion,
                UsuarioId = gasto.UsuarioId
            } );
        }

        public void Modificar(int id, GastoDTO gasto)
        {
            throw new NotImplementedException();
        }
    }
}
