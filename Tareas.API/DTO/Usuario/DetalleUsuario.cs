namespace Tareas.API.DTO.Usuario
{
    /// <summary>
    /// DTO que ayuda a  listar usuarios en  el sistema.
    /// </summary>
    public class DetalleUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string FechaCreacion { get; set; }
        public int CantidadTareas { get; set; }
    }
}