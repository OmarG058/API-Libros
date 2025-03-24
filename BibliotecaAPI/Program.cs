  using BibliotecaAPI.Datos;
using BibliotecaAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Area de servicios
builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);   //habiulitando la opcion de conroladores 

//configuramos el Applicationdbcontext como un servicio y que vamos a usar sqlServer y espesificando la cadena de conexion
builder.Services.AddDbContext<ApplicationDbContext>(
    opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));


Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");

var app = builder.Build();

app.UseLogueaPeticion();

app.UsebloqueaPeticion();

//Area de middlewears
app.MapControllers(); //cuando suceda una peticon http sea un controlador el que de respuesta y no una minimal API
//un controlador es una clase que contiene un conjunto de acciones; Y una accion es una funcion que responde
// a una peticion http




app.Run();
