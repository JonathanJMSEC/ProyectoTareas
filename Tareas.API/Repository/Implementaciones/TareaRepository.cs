using Tareas.API.Models;
using Tareas.API.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;
using Tareas.API.DTO.Tarea;

namespace Tareas.API.Repository.Implementaciones
{
    public class TareaRepository : ITareaRepository
    {
        private readonly MongoRepository<Usuario> _baseRepo;

        public TareaRepository(IMongoDatabase database)
        {
            _baseRepo = new MongoRepository<Usuario>(database, "Usuarios");
        }

        public async Task<IEnumerable<Tarea>> GetTareasAsync(string idUsuario)
        {
            Usuario usuario = await _baseRepo.GetByIdAsync(idUsuario);
            return usuario?.Tareas ?? Enumerable.Empty<Tarea>();
        }

        public async Task<Tarea> GetByIdAsync(string idTarea, string idUsuario)
        {
            Usuario usuario = await _baseRepo.GetByIdAsync(idUsuario);
            return usuario?.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
        }

        public async Task AddAsync(string idUsuario, Tarea entity)
        {
            Usuario usuario = await _baseRepo.GetByIdAsync(idUsuario);
            if (usuario == null) throw new Exception("Usuario no encotrado");

            usuario.Tareas.Add(entity);
            await _baseRepo.UpdateAsync(idUsuario, usuario);
        }

        public async Task UpdateAsync(string idUsuario, ActualizarTareaDTO tareaDto, string idTarea)
        {
            var usuario = await _baseRepo.GetByIdAsync(idUsuario);
        if (usuario == null) throw new Exception("Usuario no encontrado");

        var tareaExistente = usuario.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
        if (tareaExistente == null) throw new Exception("Tarea no encontrada");

        tareaExistente.Titulo = tareaDto.Titulo;
        tareaExistente.Descripcion = tareaDto.Descripcion;
        tareaExistente.FechaLimite = tareaDto.FechaLimite;
            tareaExistente.Id = ObjectId.Parse(idTarea);

        await _baseRepo.UpdateAsync(idUsuario, usuario);
        }

        public async Task DeleteAsync(string idUsuario, string idTarea)
        {
            var usuario = await _baseRepo.GetByIdAsync(idUsuario);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var tareaABorrar = usuario.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
            if (tareaABorrar == null) throw new Exception("Tarea no encontrada");

            usuario.Tareas.Remove(tareaABorrar);
            await _baseRepo.UpdateAsync(idUsuario, usuario);
        }
    }
}