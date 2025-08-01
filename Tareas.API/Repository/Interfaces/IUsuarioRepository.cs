using Tareas.API.Models;

namespace Tareas.API.Repository.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de usuarios.
    /// Hereda de IRepository para operaciones CRUD básicas.
    /// </summary>
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId);
    }
}