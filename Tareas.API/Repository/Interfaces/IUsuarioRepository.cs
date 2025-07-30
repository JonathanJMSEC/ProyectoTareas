using Tareas.API.Models;

namespace Tareas.API.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId);
    }
}