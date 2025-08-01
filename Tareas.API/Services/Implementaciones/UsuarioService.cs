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
            if (usuario == null)
                return ServiceResponse<Usuario>.Error("El usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Contrasena))
                return ServiceResponse<Usuario>.Error("Todos los campos son obligatorios.");

            var usuarioCreado = UsuarioMapper.ToModel(usuario);
            await _context.AddAsync(usuarioCreado);

            return ServiceResponse<Usuario>.Ok(usuarioCreado, "El usuario ha sido creado correctamente.");
        }

        public async Task<ServiceResponse<Usuario>> ObtenerUsuarioPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Usuario>.Error("El ID del usuario no puede ser nulo o vacío.");

            var usuarioObtenido = await _context.GetByIdAsync(id);

            if (usuarioObtenido == null)
                return ServiceResponse<Usuario>.Error("Usuario no encontrado.");
            
            return ServiceResponse<Usuario>.Ok(usuarioObtenido, "Usuario obtenido correctamente.");

        }

        public async Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync()
        {
            IEnumerable<Usuario> listaDeUsuarios = await _context.GetAllAsync();

            if (listaDeUsuarios == null || !listaDeUsuarios.Any())
                return ServiceResponse<IEnumerable<Usuario>>.Error("No se encontraron usuarios.");
            
            return ServiceResponse<IEnumerable<Usuario>>.Ok(listaDeUsuarios, "Usuarios obtenidos correctamente.");
        }

        public async Task<ServiceResponse<Usuario>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Usuario>.Error("El ID del usuario no puede ser nulo o vacío.");
            if (usuario == null)
                return ServiceResponse<Usuario>.Error("El usuario no puede ser nulo.");
            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Contrasena))
                return ServiceResponse<Usuario>.Error("Todos los campos son obligatorios.");

            var existingUsuario = await _context.GetByIdAsync(id);

            if (existingUsuario != null)
            {
                await _context.UpdateAsync(id, UsuarioMapper.ToModelActualizar(existingUsuario, usuario));

                existingUsuario =  await _context.GetByIdAsync(id);

                return ServiceResponse<Usuario>.Ok(existingUsuario, "Usuario actualizado correctamente.");
            }

            return ServiceResponse<Usuario>.Error("Usuario no encontrado.");
        }

        public async Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
                return ServiceResponse<IEnumerable<Tarea>>.Error("El ID del usuario no puede ser nulo o vacío.");

            IEnumerable<Tarea> tareasDelUsuario = await _context.ObtenerTareasPorUsuarioAsync(usuarioId);

            if (tareasDelUsuario == null || !tareasDelUsuario.Any())
                return ServiceResponse<IEnumerable<Tarea>>.Error("No se encontraron tareas para el usuario especificado.");
            
            return ServiceResponse<IEnumerable<Tarea>>.Ok(tareasDelUsuario, "Tareas obtenidas correctamente.");
        }
    }
}