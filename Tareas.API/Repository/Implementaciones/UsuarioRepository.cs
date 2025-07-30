using Tareas.API.Models;
using Tareas.API.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Tareas.API.Repository.Implementaciones
{
    public class UsuarioRepository : MongoRepository<Usuario>, IRepository<Usuario>
    {
        public UsuarioRepository(IMongoDatabase database) : base(database, "Usuarios")
        {
        }

        public async Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            var filter = Builders<Usuario>.Filter.Eq("_id", ObjectId.Parse(usuarioId));
            var usuario = await Collection.Find(filter).FirstOrDefaultAsync();

            return usuario.Tareas;
        }
    }
}