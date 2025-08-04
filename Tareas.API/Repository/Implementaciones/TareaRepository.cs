using Tareas.API.Models;
using Tareas.API.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;
using Tareas.API.DTO.Tarea;

namespace Tareas.API.Repository.Implementaciones
{
    public class TareaRepository : MongoRepository<Usuario>, IRepository<Usuario>
    {

        public TareaRepository(IMongoDatabase database) : base(database, "Usuarios") { }

        public async Task<IEnumerable<Tarea>> GetTareasAsync(string idUsuario)
        {
            Usuario usuario = await base.GetByIdAsync(idUsuario);
            return usuario?.Tareas ?? Enumerable.Empty<Tarea>();
        }

        public async Task<Tarea> GetByIdAsync(string idTarea, string idUsuario)
        {
            Usuario usuario = await base.GetByIdAsync(idUsuario);
            return usuario?.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
        }

        public async Task AddAsync(string idUsuario, Tarea entity)
        {
            Usuario usuario = await GetByIdAsync(idUsuario);
            if (usuario == null) throw new Exception("Usuario no encotrado");

            usuario.Tareas.Add(entity);
            await base.UpdateAsync(idUsuario, usuario);
        }

        public async Task UpdateAsync(string idUsuario, CrearTareaDTO tareaDto, string idTarea)
        {
            var usuario = await base.GetByIdAsync(idUsuario);
        if (usuario == null) throw new Exception("Usuario no encontrado");

        var tareaExistente = usuario.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
        if (tareaExistente == null) throw new Exception("Tarea no encontrada");

        tareaExistente.Titulo = tareaDto.Titulo;
        tareaExistente.Descripcion = tareaDto.Descripcion;
        tareaExistente.FechaLimite = tareaDto.FechaLimite;

        await base.UpdateAsync(idUsuario, usuario);
        }

        public async Task DeleteAsync(string idUsuario, string idTarea)
        {
            var usuario = await base.GetByIdAsync(idUsuario);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var tareaABorrar = usuario.Tareas.FirstOrDefault(t => t.Id == ObjectId.Parse(idTarea));
            if (tareaABorrar == null) throw new Exception("Tarea no encontrada");

            usuario.Tareas.Remove(tareaABorrar);
            await base.UpdateAsync(idUsuario, usuario);
        }
    }
}