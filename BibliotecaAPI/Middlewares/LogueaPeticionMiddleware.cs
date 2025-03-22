namespace BibliotecaAPI.Middlewares
{
    public class LogueaPeticionMiddleware
    {
        private readonly RequestDelegate next;

        public LogueaPeticionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            //Esto sucede cuando viene la peticion http
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();    //Guardando informacion acerca de la peticion http
            logger.LogInformation($"Peticion: {context.Request.Method} {context.Request.Path}");

            await next.Invoke(context);//Esto lo pone en espera y va hacia map controllers

            //Esto sucede cuando ya se ha retornado un valor de alguna peticion 
            logger.LogInformation($"Respuesta:{context.Response.StatusCode}");//Guardando informacion acerca de la respuesta 


            //Esta classe se crea para evitar tener mucho codio en   la clase program


            //app.Use(async (context, next) => { //Basicamente sirve para visualizar las salidas de las peticiones http

            //    //Esto sucede cuando viene la peticion http
            //    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();    //Guardando informacion acerca de la peticion http
            //    logger.LogInformation($"Peticion: {context.Request.Method} {context.Request.Path}");

            //    await next.Invoke();//Esto lo pone en espera y va hacia map controllers

            //    //Esto sucede cuando ya se ha retornado un valor de alguna peticion 
            //    logger.LogInformation($"Respuesta:{context.Response.StatusCode}");//Guardando informacion acerca de la respuesta 
            //});

        }
    }

    public static class LogueaPeticionMiddlewareExtensions  
    {
        public static IApplicationBuilder UseLogueaPeticion(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<LogueaPeticionMiddleware>();   
        }
    }
}
