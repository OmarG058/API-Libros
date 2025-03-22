using BibliotecaAPI.Datos;
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

        public AutoresController(ApplicationDbContext context,ILogger <AutoresController> logger) //shorthand ctor tab crea contructor 
        {
            this.context = context; //control punto crear y asignar campo
            this.logger = logger;
        }



        [HttpGet]
        //retornamos un IEnumerable de los autores que seria una colecion Obtener todos losArores
        public async Task<IEnumerable<Autor>> Get() {
            logger.LogTrace("obteniendo el listado de autores");
            logger.LogInformation("obteniendo listado de autores");//Ilogger permite mostar mesajes de lo que esta ocurriendo en la aplicacion 
            logger.LogWarning("obteniendo el listado de autores"); //puedo modificarlos desde appsetingsJson para elegir cual se ejecute 
            logger.LogCritical("obteniendo listado de autores");  //por defaoult es information. usa trace para bugs

            return await context.Autores.ToListAsync<Autor>();
        }

        //REGLAS DE RUTEO

        [HttpGet("{nombre:alpha}")]//Ruteo con restricion de la variable de ruta alpha = string los demas tipo de datos son igual
        public async Task<IEnumerable<Autor>> Get(string nombre) 
        {
            return await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        
        }
        //[HttpGet("{R1}/{R2?}")]
        //public ActionResult Get(string R1,string R2 = "Valor por defecto") 
        //{
            
        //    return Ok(new { R1, R2 });  
        //}

        [HttpGet("{id:int}", Name ="ObtenerAutor" ) ] // api/autores/id/id?incluirLibros=true | false
        public async Task<ActionResult<Autor>> Get([FromRoute] int id) //Optener Autor  por id
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost]
        //la proramacion async me permite trabajar de mejor manera con operacion IO con operacion de nuestro sistemas hacia otros
        public async Task<ActionResult> Post([FromBody]Autor autor)//Añadir Autor
        {
            context.Add(autor);
            await context.SaveChangesAsync();   //cuando tenemos una operacion IO debes tener esta linea para que se gurden los combios
            return CreatedAtRoute("ObtenerAutor",new {id =autor.Id},autor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor) //modificar por id id
        {
            if (id != autor.Id)
            {
                return BadRequest("los id deben de coincidir");
            }
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
