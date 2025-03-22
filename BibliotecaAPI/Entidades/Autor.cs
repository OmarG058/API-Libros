using BibliotecaAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Autor:IValidatableObject  //Utilza esta clase para crear VALIDACIONES POR MODELO 
    {
        public int Id { get; set; }
        [StringLength(90, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")] //Estas son validaciones por atributo
        [Required(ErrorMessage = "El campo {0} nombre es Requerido")] //indicamos asp que si viene un autor desde uncliente debe tener un nombre si no lo tiene se rechaza la conexion
        // [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) //VALIDACION DE PROPIEDADES MEDIANTE MODELOS
        {
            if (!string.IsNullOrEmpty(Nombre) ) //&& Edad =>18 puedes usarlo tambien directamente juantando varias propidades
            {
                    var primeraLetra = Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("la primera letra debe ser mayuscula - por modelo", new string[] { nameof(Nombre) });
                }
            }
        }

        //[Range (18,120)]
        //public int Edad { get; set; }

        //[CreditCard]
        //public string? CredtCard { get; set; }

        //[Url]
        //public string? URL { get; set; }
    }
}
