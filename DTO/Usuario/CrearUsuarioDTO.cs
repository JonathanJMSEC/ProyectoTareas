using System.ComponentModel.DataAnnotations;

namespace DTO.Usuario
{
    public class CrearUsuarioDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}