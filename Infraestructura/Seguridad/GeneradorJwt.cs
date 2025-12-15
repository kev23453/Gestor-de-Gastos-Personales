using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Seguridad;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructura.Seguridad
{
    public class GeneradorJwt : IGeneradorJwt
    {
        private readonly IConfiguration configuration;
        public GeneradorJwt(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateJwt(UsuarioDTO usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("Username", usuario.username),
                new Claim("Email", usuario.email)
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
