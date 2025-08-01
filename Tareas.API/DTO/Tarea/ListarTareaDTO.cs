namespace Tareas.API.DTO.Tarea
{
    /// <summary>
    /// DTO para listar las tareas en el sistema.   
    /// /// Contiene propiedades para el título, descripción, estado, fecha de creación y fecha límite.
    /// /// </summary>
    public class ListarTareaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}