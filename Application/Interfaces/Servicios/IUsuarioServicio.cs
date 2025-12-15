using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Dominio.Entidades;

namespace Application.Interfaces.Servicios
{
    public interface IUsuarioServicio
    {
        void Registrar(RegisterDTO registrar);
        UsuarioDTO Login(LoginDTO login);
        void Perfil(ActualizarPerfilDTO dto, int id);
    }
}
