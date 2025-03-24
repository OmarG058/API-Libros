using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]//para saber a que url hay que apuntar para llamar a las acciones de este controlador 
    public class AutoresController : ControllerBase //ControllerBase Me permite trabajar de maner sencilla co web APIS
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<AutoresController> logger;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context,ILogger <AutoresController> logger,IMapper mapper) //shorthand ctor tab crea contructor 
        {
            this.context = context; //control punto crear y asignar campo
            this.logger = logger;
            this.mapper = mapper;
        }



        [HttpGet]
        //retornamos un IEnumerable de los autores que seria una colecion Obtener todos losArores
        public async Task<IEnumerable<AutorDTO>> Get() {
       

            var autores= await context.Autores.ToListAsync<Autor>();
            var autoresDTO = mapper.Map<IEnumerable<AutorDTO>>(autores);
            return autoresDTO;
        }

        //REGLAS DE RUTEO

        [HttpGet("{nombre:alpha}")]//Ruteo con restricion de la variable de ruta alpha = string los demas tipo de datos son igual
        public async Task<IEnumerable<Autor>> Get(string nombre) 
        {
            return await context.Autores.Where(x => x.Nombres.Contains(nombre)).ToListAsync();
        
        }
        //[HttpGet("{R1}/{R2?}")]
        //public ActionResult Get(string R1,string R2 = "Valor por defecto") 
        //{
            
        //    return Ok(new { R1, R2 });  
        //}

        [HttpGet("{id:int}", Name ="ObtenerAutor" ) ] // api/autores/id/id?incluirLibros=true | false
        public async Task<ActionResult<AutorDTO>> Get([FromRoute] int id) //Optener Autor  por id
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();
            }
            var autorDTO =mapper.Map<AutorDTO>(autor);
            return autorDTO;
        }

        [HttpPost]
        //la proramacion async me permite trabajar de mejor manera con operacion IO con operacion de nuestro sistemas hacia otros
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)//Añadir Autor
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            context.Add(autor);
            await context.SaveChangesAsync();   //cuando tenemos una operacion IO debes tener esta linea para que se gurden los combios

            var autorDTO = mapper.Map<AutorDTO>(autor); 
            return CreatedAtRoute("ObtenerAutor",new {id =autor.Id}, autorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AutorCreacionDTO autorCreacionDTO) //modificar por id id
        {
            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;
            
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            //llendo a la bd y buscar todos los autores que contengan el id igual al id y borarralos 
            // y retornamos la cantidad de los registrod borrados 
            var registroBorrados = await context.Autores.Where(x=> x.Id == id).ExecuteDeleteAsync();
            if (registroBorrados == 0) 
            {
                return NotFound();//ningun registro fue borrado es decir no existe el id espesifocado
            }
            return Ok();
 
        }   
    }
}
