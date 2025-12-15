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

namespace Application.Servicios
{
    public class MetodoPagoServicio : IMetodoPagoServicio
    {
        private readonly IMetodoPagoRepositorio repositorio;
        private readonly IGastoRepositorio gastoRepositorio;
        public MetodoPagoServicio(IMetodoPagoRepositorio repositorio, IGastoRepositorio _gastoRepositorio)
        {
            gastoRepositorio = _gastoRepositorio;
            this.repositorio = repositorio;
        }

        public void Actualizar(int id, ActualizarMetodoPagoDTO metodo)
        {
            var NuevoMetodo = new MetodoPago
            {
                Descripcion = metodo.Descripcion
            };
            repositorio.update(id, NuevoMetodo);
        }

        public void crearMetodo(CrearMetodoPagoDTO metodo)
        {
            var NuevoMetodo = new MetodoPago
            {
                Descripcion = metodo.Descripcion
            };
            repositorio.Insert(NuevoMetodo);
        }

        public void Eliminar(int id)
        {
            var buscarGastos = gastoRepositorio.get(id);
            if(buscarGastos.MetodoPagoId == id)
            {
                throw new BusinessException("Este metodo de pago posee gastos asociados");
            }
            repositorio.delete(id);
        }

        public IEnumerable<MetodoPagoDTO> ObtenerMetodos()
        {
            return repositorio.GetAll().Select(Metodo => new MetodoPagoDTO { Descripcion = Metodo.Descripcion }).ToList();
        }

        public MetodoPagoDTO ObtenerPorId(int id)
        {
            var metodoPago = repositorio.Get(id);
            return new MetodoPagoDTO
            {
                Descripcion = metodoPago.Descripcion
            };
        }
    }
}
