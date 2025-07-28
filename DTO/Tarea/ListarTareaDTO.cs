namespace DTO.Tarea
{
    public class ListarTareaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}