using Tareas.API.DTO.Usuario;

using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ServiceResponse<Usuario>> CrearUsuarioAsync(CrearUsuarioDTO usuario);
        Task<ServiceResponse<Usuario>> ObtenerUsuarioPorIdAsync(string id);
        Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync();
        Task<ServiceResponse<Usuario>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario);
        Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId);
    }
}
