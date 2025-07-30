using Tareas.API.DTO.Usuario;

using Tareas.API.Models;

namespace Tareas.API.Services
{
    public interface IUsuarioService
    {
        public Task CrearUsuarioAsync(CrearUsuarioDTO usuario);
        public Task<Usuario> ObtenerUsuarioPorIdAsync(string id);
        public Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync();
        public Task<IEnumerable<Tarea>> ObtenerTareasUsuarioAsync(string id);
    }
}
