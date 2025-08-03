using Tareas.API.DTO.Usuario;

using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ServiceResponse<ResumenUsuarioDTO>> CrearUsuarioAsync(CrearUsuarioDTO usuario);
        Task<ServiceResponse<ResumenUsuarioDTO>> ObtenerUsuarioPorIdAsync(string id);
        Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync();
        Task<ServiceResponse<ResumenUsuarioDTO>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario);
        Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId);
    }
}
