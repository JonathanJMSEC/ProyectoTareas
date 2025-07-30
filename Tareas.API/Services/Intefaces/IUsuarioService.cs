using Tareas.API.DTO.Usuario;

using Tareas.API.Models;

namespace Tareas.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CrearUsuarioAsync(CrearUsuarioDTO usuario);
        Task<Usuario> ObtenerUsuarioPorIdAsync(string id);
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync();
        Task ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario);
        Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId);
    }
}
