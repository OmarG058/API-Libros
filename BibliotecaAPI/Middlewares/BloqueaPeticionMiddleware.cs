using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace BibliotecaAPI.Middlewares
{
    public class BloqueaPeticionMiddleware
    {
        private readonly RequestDelegate next;

        public BloqueaPeticionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/bloqueado")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Acceso denegado");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }

    public static class BloqueaPeticionMiddleWareExtencions
    {
        public static IApplicationBuilder UsebloqueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BloqueaPeticionMiddleware>();
        }
    }

}


