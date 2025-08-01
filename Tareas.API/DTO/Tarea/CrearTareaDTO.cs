using System.ComponentModel.DataAnnotations;

namespace Tareas.API.DTO.Tarea
{
    public class CrearTareaDTO
    {
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaLimite { get; set; }
    }
}