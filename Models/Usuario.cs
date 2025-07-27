using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace proyectoTareas.Models
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

        public Usuario() { }

        public Usuario(ObjectId id, string nombre, string email, string passwordHash)
        {
            Id = id;
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
    }
}