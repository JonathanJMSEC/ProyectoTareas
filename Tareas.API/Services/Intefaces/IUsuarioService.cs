using Tareas.API.DTO.Usuario;

using Tareas.API.Models;

namespace Tareas.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CrearUsuarioAsync(CrearUsuarioDTO usuario);
        Task<Usuario> ObtenerUsuarioPorIdAsync(string id);
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync();
        Task<IEnumerable<Tarea>> ObtenerTareasUsuarioAsync(string id);
    }
}
