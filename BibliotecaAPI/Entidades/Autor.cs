using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [StringLength(90, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        [Required(ErrorMessage = "El campo {0} nombre es Requerido")] //indicamos asp que si viene un autor desde uncliente debe tener un nombre si no lo tiene se rechaza la conexion
        public required string Nombre { get; set; }

        public List<Libro> Libros { get; set; } = new List<Libro>();

        //[Range (18,120)]
        //public int Edad { get; set; }

        //[CreditCard]
        //public string? CredtCard { get; set; }

        //[Url]
        //public string? URL { get; set; }
    }
}
