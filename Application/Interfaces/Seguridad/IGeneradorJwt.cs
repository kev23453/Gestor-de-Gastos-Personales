using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces.Seguridad
{
    public interface IGeneradorJwt
    {
        string GenerateJwt(UsuarioDTO usuario);
    }
}
