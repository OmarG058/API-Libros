using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
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
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get() // te devuelve los libros
        {
            var libros = await context.Libros.Include(x => x.Autor).ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;

        }

        [HttpGet("{id:int}",Name ="ObtenerLibros")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await context.Libros.Include(x=>x.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }

            var libroDTO = mapper.Map<LibroDTO>(libro);
            return libroDTO;

        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);    
            var Existeautor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            //REALIZANDO UNA VALIDACION POR CONTROLADOR 

            ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId} no exitse");
            if (Existeautor == false)
            {
                return ValidationProblem();
            }
            context.Libros.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("ObtenerLibros", new{id = libro.Id}, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO) 
        {
            var libro = mapper.Map<Libro>(libroCreacionDTO);
            libro.Id = id;  
           
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
