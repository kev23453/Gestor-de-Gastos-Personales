using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositorios;
using Application.Excepciones;
using Application.Interfaces.Servicios;
using Dominio.Entidades;

namespace Application.Servicios
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly ICategoriaRepositorio categoriaRepositorio;
        private readonly IGastoRepositorio gastoRepositorio;
        public CategoriaServicio(ICategoriaRepositorio _categoriaRepositorio, IGastoRepositorio _gastoRepositorio)
        {
            categoriaRepositorio = _categoriaRepositorio;
            gastoRepositorio = _gastoRepositorio;
        }

        public void Crear(int Usuario, CategoriaDTO categoria)
        {
            var categorias = categoriaRepositorio.GetAll();
            if(categorias.Any(cat => cat.Descripcion == categoria.Descripcion))
            {
                throw new BusinessException("Ya existe esta categoria");
            }

            var Nuevacategoria = new Categoria
            {
                Descripcion = categoria.Descripcion,
                Presupuesto = categoria.Presupuesto,
                Id_usuario = categoria.Usuario,
                estadoActivo = categoria.estado
            };
            categoriaRepositorio.Insertar(Nuevacategoria);
        }
        
        public void Editar(int id, CategoriaDTO categoria)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(EliminarCategoriaDTO eliminarCategoriaDTO)
        {
            var gastosXcategoria = gastoRepositorio.GastoXcategoria(eliminarCategoriaDTO.Id);
            if(gastosXcategoria != null)
            {
                throw new BusinessException("Esta categoria no puede eliminarse, posee gastos asociados");
            }
            categoriaRepositorio.Eliminar(eliminarCategoriaDTO.Id);
        }

        public CategoriaDTO ObtenerPorID(int id)
        {
            var categoria = categoriaRepositorio.GetCategoria(id);
            if(categoria == null)
            {
                throw new BusinessException($"No se encuentran categorias con el id: {id}");
            }
            return new CategoriaDTO
            {
                Descripcion = categoria.Descripcion,
                Presupuesto = categoria.Presupuesto,
                Usuario = categoria.Id_usuario,
                estado = categoria.estadoActivo
            };
        }

        public IEnumerable<CategoriaDTO> ObtenerTodos()
        {
            return categoriaRepositorio.GetAll().Select(categoria => new CategoriaDTO
            {
                Descripcion = categoria.Descripcion,
                Presupuesto = categoria.Presupuesto,
                estado = categoria.estadoActivo,
                Usuario = categoria.Id_usuario
            });
        }
    }
}
