using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tareas.API.Models
{
    public class Tarea
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("titulo")]
        public string Titulo { get; set; }
        [BsonElement("descripcion")]
        public string Descripcion { get; set; }
        [BsonElement("Estado")]
        public string Estado { get; set; }
        [BsonElement("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }
        [BsonElement("fechaLimite")]
        public DateTime FechaLimite { get; set; }
        
        public Tarea(string titulo, string descripcion, string estado, DateTime fechaLimite)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Estado = estado;
            FechaLimite = fechaLimite;
            FechaCreacion = DateTime.Now;
        }


        // Metodos
        public void ActualizarEstado(string nuevoEstado)
        {
            if (string.IsNullOrEmpty(nuevoEstado))
                throw new ArgumentException("El nuevo estado no puede ser nulo o vacío.");

            Estado = nuevoEstado;
        }

        public void ActualizarFechaLimite(DateTime nuevaFechaLimite)
        {
            if (nuevaFechaLimite < FechaCreacion)
                throw new ArgumentException("La nueva fecha límite no puede ser anterior a la fecha de creación.");

            FechaLimite = nuevaFechaLimite;
        }

        public void ActualizarDescripcion(string nuevaDescripcion)
        {
            if (string.IsNullOrEmpty(nuevaDescripcion))
                throw new ArgumentException("La nueva descripción no puede ser nula o vacía.");

            Descripcion = nuevaDescripcion;
        }

        public bool EstaAtrsada()
        {
            return DateTime.Now > FechaLimite && Estado != "Completada";
        }

        public void Actualizar(string nuevoTitulo, string nuevaDescripcion, string nuevoEstado, DateTime nuevaFechaLimite)
        {
            ActualizarDescripcion(nuevaDescripcion);
            ActualizarEstado(nuevoEstado);
            ActualizarFechaLimite(nuevaFechaLimite);

            if (!string.IsNullOrEmpty(nuevoTitulo))
                throw new ArgumentException("El título no puede ser nulo o vacío.");

            Titulo = nuevoTitulo;
        }

        public override string ToString()
        {
            return $"Tarea: {Titulo}\n Descripción: {Descripcion}\n Estado: {Estado}\n Fecha de Creación: {FechaCreacion}\n Fecha Límite: {FechaLimite}";
        }
    }
}