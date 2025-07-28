namespace DTO.Tarea
{ 
    public class ActualizarTareaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Estado { get; set; }
    }
}