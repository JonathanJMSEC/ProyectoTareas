namespace Tareas.API.DTO.Tarea
{
    /// <summary>
    /// DTO que representa un resumen de una tarea en el sistema.
    /// </summary>
    public class ResumenTareaDTO
    {
        public string Titulo { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
    }
}