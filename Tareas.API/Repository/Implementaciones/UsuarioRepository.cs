using Tareas.API.Models;
using Tareas.API.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Tareas.API.Repository.Implementaciones
{
    /// <summary>
    /// Implementación del repositorio de usuarios.
    /// Hereda de MongoRepository para operaciones CRUD básicas.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MongoRepository<Usuario> _baseRepo;
        public UsuarioRepository(IMongoDatabase database)
        {
            _baseRepo = new MongoRepository<Usuario>(database, "Usuarios");
        }

        public Task AddAsync(Usuario entity) => _baseRepo.AddAsync(entity);

        public Task DeleteAsync(string id) => _baseRepo.DeleteAsync(id);

        public Task<IEnumerable<Usuario>> GetAllAsync() => _baseRepo.GetAllAsync();

        public Task<Usuario> GetByIdAsync(string id) => _baseRepo.GetByIdAsync(id);

        public async Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            Usuario usuario = await _baseRepo.GetByIdAsync(usuarioId);

            return usuario?.Tareas ?? Enumerable.Empty<Tarea>();
        }

        public Task UpdateAsync(string id, Usuario entity) => _baseRepo.UpdateAsync(id, entity);
    }
}