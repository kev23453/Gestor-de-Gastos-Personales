using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Servicios;
using Application.Utils;
using Application.Excepciones;
using Dominio.Entidades;
using System.Collections.ObjectModel;

namespace Application.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        public UsuarioServicio(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public UsuarioDTO Login(LoginDTO login)
        {
            var email = login.email;
            var password = login.password;
            var usuario = _usuarioRepo.verificarUsuario(email);
            if(usuario == null)
            {
                throw new BusinessException("Usuario no encontrado");
            }
            bool verificacion = UtHash.verify(login.password, usuario.password);
            var login_exec = _usuarioRepo.ObtenerLoginUsuario(email, verificacion);

            if(login_exec == null)
            {
                throw new BusinessException("Credenciales incorrectas");
            }
            
            return new UsuarioDTO
            {
                Id = login_exec.Id,
                username = login_exec.username,
                email = login_exec.email
            };
        }

        public void Perfil(ActualizarPerfilDTO dto, int id)
        {
            var usuario = _usuarioRepo.getById(id);
            if(usuario == null)
            {
                throw new BusinessException("Usuario no encontrado");
            }
            var usuarioModificado = new Usuario
            {
                Id = usuario.Id,
                username = dto.username,
                email = usuario.email,
                password = UtHash.Hash(dto.password)
            };
            _usuarioRepo.Modificar(usuarioModificado);
        }

        public void Registrar(RegisterDTO registrar)
        {
            if(registrar.username == null || registrar.email == null || registrar.password == null)
            {
                throw new BusinessException("Todos los campos son obligatorios");
            }
            if (_usuarioRepo.verificarUsuario(registrar.email) != null)
            {
                throw new BusinessException("Este usuario ya esta registrado");
            }
            var usuario = new Usuario
            {
                username = registrar.username,
                email = registrar.email,
                password = UtHash.Hash(registrar.password)
            };
            _usuarioRepo.insertar(usuario);
        }
    }
}
