using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class AutorCreacionDTO
    {
        [StringLength(90, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")] //Estas son validaciones por atributo
        [Required(ErrorMessage = "El campo {0} nombre es Requerido")] //indicamos asp que si viene un autor desde uncliente debe tener un nombre si no lo tiene se rechaza la conexion
        // [PrimeraLetraMayuscula]
        public required string Nombres { get; set; } //shorthan CTRR+R + CTRL+R MODIFICAS LA PROPIEDAD EN DONDE SEA  QUE SE ESTE USANDO 

        [StringLength(90, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        [Required(ErrorMessage = "El campo {0} nombre es Requerido")]
        public required string Apellidos { get; set; }

        [StringLength(20, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public string? Identificacion { get; set; }
    }
}
