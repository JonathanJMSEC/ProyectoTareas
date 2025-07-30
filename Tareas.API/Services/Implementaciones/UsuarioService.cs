using Tareas.API.Models;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Usuario;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;

namespace Tareas.API.Services.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _context;

        public UsuarioService(IUsuarioRepository context)
        {
            _context = context;
        }

        public async Task CrearUsuarioAsync(CrearUsuarioDTO usuario)
        {
            await _context.AddAsync(UsuarioMapper.ToModel(usuario));
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(string id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosLosUsuariosAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario)
        {
            var existingUsuario = await _context.GetByIdAsync(id);

            if (existingUsuario != null)
                await _context.UpdateAsync(id, UsuarioMapper.ToModelActualizar(existingUsuario, usuario));

        }
        
        public async Task<IEnumerable<Tarea>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            return await _context.ObtenerTareasPorUsuarioAsync(usuarioId);
        }
    }
}