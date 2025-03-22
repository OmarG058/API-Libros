using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(90, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        public required string Titulo { get; set; }
        [Required]
        public required int AutorId { get; set; }
        public Autor? Autor { get; set; }
    }
}
