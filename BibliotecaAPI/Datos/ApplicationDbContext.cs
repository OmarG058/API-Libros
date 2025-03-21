using BibliotecaAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Datos
{
    public class ApplicationDbContext:DbContext //Esta clase el pieza principal donde tenemos las configuraciones de EntityFramewokCore
    {                                           //ctrl+. generas el constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }  //shorthand prop+tab y te crea la propiedad
        public DbSet<Libro> Libros { get; set; }
    }
}
