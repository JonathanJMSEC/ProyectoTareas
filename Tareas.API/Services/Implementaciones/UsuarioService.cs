using Tareas.API.Models;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Usuario;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;
using Tareas.API.Services.Helpers;

namespace Tareas.API.Services.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _context;

        public UsuarioService(IUsuarioRepository context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Usuario>> CrearUsuarioAsync(CrearUsuarioDTO usuario)
        {
            ValidacionesUsuario.EsUsuarioValido(usuario);

            var usuarioCreado = UsuarioMapper.ToModel(usuario);

            await _context.AddAsync(usuarioCreado);

            return ServiceResponse<Usuario>.Ok(usuarioCreado, "El usuario ha sido creado correctamente.");
        }

        public async Task<ServiceResponse<Usuario>> ObtenerUsuarioPorIdAsync(string id)
        {
            ValidacionesUsuario.EsIdValido(id);

            var usuarioObtenido = await _context.GetByIdAsync(id);

            ValidacionesUsuario.EsUsuarioExistente(usuarioObtenido);
            
            return ServiceResponse<Usuario>.Ok(usuarioObtenido, "Usuario obtenido correctamente.");

        }

        public async Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync()
        {
            IEnumerable<Usuario> listaDeUsuarios = await _context.GetAllAsync();

            ValidacionesUsuario.EsListaDeUsuariosValida(listaDeUsuarios);
            
            return ServiceResponse<IEnumerable<Usuario>>.Ok(listaDeUsuarios, "Usuarios obtenidos correctamente.");
        }

        public async Task<ServiceResponse<Usuario>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario)
        {
            ValidacionesUsuario.EsIdValido(id);
            ValidacionesUsuario.EsUsuarioValido(usuario);

            var existingUsuario = await _context.GetByIdAsync(id);

            ValidacionesUsuario.EsUsuarioExistente(existingUsuario);

            await _context.UpdateAsync(id, UsuarioMapper.ToModelActualizar(existingUsuario, usuario));

            existingUsuario =  await _context.GetByIdAsync(id);

            return ServiceResponse<Usuario>.Ok(existingUsuario, "Usuario actualizado correctamente.");
            
        }

        public async Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            ValidacionesUsuario.EsIdValido(usuarioId);

            IEnumerable<Tarea> tareasDelUsuario = await _context.ObtenerTareasPorUsuarioAsync(usuarioId);

            ValidacionesTarea.EsListaDeTareasValida(tareasDelUsuario);
            
            return ServiceResponse<IEnumerable<Tarea>>.Ok(tareasDelUsuario, "Tareas obtenidas correctamente.");
        }
    }
}