using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tareas.API.Models
{
    public class Usuario
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("passwordHash")]
        public string PasswordHash { get; private set; }
        [BsonElement("fechaRegistro")]
        public DateTime FechaRegistro { get; set; }
        [BsonElement("tareas")]
        public List<Tarea> Tareas { get; set; } = new List<Tarea>();

        public Usuario(string nombre, string email, string passwordHash)
        {
            Nombre = nombre;
            Email = email;
            FechaRegistro = DateTime.Now;
            PasswordHash = passwordHash;
        }

        // Metodos
        public void ActualizarEmail(string nuevoEmail)
        {
            if (string.IsNullOrEmpty(nuevoEmail))
                throw new ArgumentException("El nuevo email no puede ser nulo o vacío.");

            Email = nuevoEmail;
        }

        public void ActualizarNombre(string nuevoNombre)
        {
            if (string.IsNullOrEmpty(nuevoNombre))
                throw new ArgumentException("El nuevo nombre no puede ser nulo o vacío.");

            Nombre = nuevoNombre;
        }

        public void AgregarTarea(Tarea tarea)
        {
            if (tarea == null)
                throw new ArgumentNullException(nameof(tarea), "La tarea no puede ser nula.");

            Tareas.Add(tarea);
        }

        public void EliminarTarea(Tarea tarea)
        {
            if (tarea == null)
                throw new ArgumentNullException(nameof(tarea), "La tarea no puede ser nula.");

            Tareas.Remove(tarea);
        }

        public Tarea ObtenerTareaPorId(ObjectId tareaId)
        {
            return Tareas.FirstOrDefault(t => t.Id == tareaId);
        }

        public void ActualizarPassword(string nuevoPasswordHash)
        {
            if (string.IsNullOrEmpty(nuevoPasswordHash))
                throw new ArgumentException("El nuevo password no puede ser nulo o vacío.");

            PasswordHash = nuevoPasswordHash;
        }
    }
}