using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Libro>> Get() // te devuelve los libros
        {
            return await context.Libros.Include(x => x.Autor).ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros.Include(x=>x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }
            return libro;

        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var Existeautor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (Existeautor == false)
            {
                return BadRequest($"El autor de id {libro.AutorId} no exitse");
            }
            context.Libros.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,Libro libro) 
        {
            if (id != libro.Id) 
            {
                return BadRequest($"El id {id} del libro no coincide");    
            }//verificar que el libro exista

            var Existeautor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);//verificar que el autor exista

            if (Existeautor == false)
            {
                return BadRequest($"El autor de id {libro.AutorId} no exitse");
            }
            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();    
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var libroseliminados = await context.Libros.Where(x=>x.Id == id).ExecuteDeleteAsync();
            if (libroseliminados == 0) 
            {
                return NotFound($"El libro a eliminar no exite ");
            }
             return Ok();   
        
        }
    }
}
