using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public required string Titulo { get; set; }
        [Required]
        public required int AutorId { get; set; }
        public Autor? Autor { get; set; }
    }
}
