using Application.Excepciones;
using Dominio.Exepciones;
using Infraestructura.Persistencia.Excepciones;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace KostoBack.Middlewares
{
    public class MiddlewareExcepcion
    {
        private readonly RequestDelegate _next;
        public MiddlewareExcepcion(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await managerException(httpContext, ex);
            }
        }

        private Task managerException(HttpContext context, Exception exception)
        {
            int codigo;
            string mensaje = exception.Message;

            switch(exception)
            {
                case DomainExeption:
                    codigo = StatusCodes.Status400BadRequest; break;
                case BusinessException:
                    codigo = StatusCodes.Status404NotFound; break;
                case InfraestructureExcepcion:
                    codigo = StatusCodes.Status500InternalServerError; break;
                default:
                    codigo = StatusCodes.Status500InternalServerError; break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = codigo;

            return context.Response.WriteAsJsonAsync(new
            {
                success = false,
                mensaje = mensaje
            });

        }
        
    }
}
