namespace BibliotecaAPI.DTOs
{
    public class AutorDTO
    {
        //colocamoa en el DTO solo lo que quieres que el usuario vea
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public List<LibroDTO> Libros { get; set; } = [];
    }
}
