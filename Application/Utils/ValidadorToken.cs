using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public class ValidadorToken
    {
        private readonly IHttpContextAccessor _httpClientFactory;
        public ValidadorToken(IHttpContextAccessor httpclientefactory)
        {
            _httpClientFactory = httpclientefactory;
        }
        public bool VerificarToken()
        {
            var identity = _httpClientFactory.HttpContext.User.Identity as ClaimsIdentity;

            if (identity.Claims.Count() == 0)
            {
                return false;
            }

            return true;
        }
        public int TokenID()
        {
            var identity = _httpClientFactory.HttpContext.User.Identity as ClaimsIdentity;
            var id = int.Parse(identity.FindFirst("id").Value);
            return id;
        }
        public string TokenNombre()
        {
            var identity = _httpClientFactory.HttpContext.User.Identity as ClaimsIdentity;
            var nombre = identity.FindFirst("nombre").Value;
            return nombre;
        }
        public string TokenEmail()
        {
            var identity = _httpClientFactory.HttpContext.User.Identity as ClaimsIdentity;
            var email = identity.FindFirst("email").Value;
            return email;
        }
    }
}