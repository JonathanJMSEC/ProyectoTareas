using Tareas.API.Models;
using Tareas.API.Services.Interfaces;
using Tareas.API.DTO.Usuario;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Mappers;
using Tareas.API.Services.Helpers;

namespace Tareas.API.Services.Implementaciones
{
    /// <summary>
    /// Implementación del servicio de usuarios.
    /// Hereda de IUsuarioService para definir operaciones relacionadas con usuarios.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _context;

        public UsuarioService(IUsuarioRepository context)
        {
            _context = context;
        }
        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// Valida los datos del usuario y lo agrega a la base de datos.
        /// </summary>
        /// <param name="usuario">Los datos del usuario a crear</param>
        /// <returns>Un resultado  indicando si se completo la  tarea</returns>
        public async Task<ServiceResponse<Usuario>> CrearUsuarioAsync(CrearUsuarioDTO usuario)
        {
            ValidacionesUsuario.EsUsuarioValido(usuario);

            var usuarioCreado = UsuarioMapper.ToModel(usuario);

            await _context.AddAsync(usuarioCreado);

            return ServiceResponse<Usuario>.Ok(usuarioCreado, "El usuario ha sido creado correctamente.");
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// Valida que el ID sea correcto y que el usuario exista.
        /// </summary>
        /// <returns>Respuesta con el usuario obtenido y mensaje de éxito o error.</returns>    
        public async Task<ServiceResponse<Usuario>> ObtenerUsuarioPorIdAsync(string id)
        {
            ValidacionesUsuario.EsIdValido(id);

            var usuarioObtenido = await _context.GetByIdAsync(id);

            ValidacionesUsuario.EsUsuarioExistente(usuarioObtenido);

            return ServiceResponse<Usuario>.Ok(usuarioObtenido, "Usuario obtenido correctamente.");

        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema. 
        /// Valida que la lista de usuarios no esté vacía.
        /// </summary>
        /// <returns>Una lista de usuarios</returns>
        public async Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync()
        {
            IEnumerable<Usuario> listaDeUsuarios = await _context.GetAllAsync();

            ValidacionesUsuario.EsListaDeUsuariosValida(listaDeUsuarios);

            return ServiceResponse<IEnumerable<Usuario>>.Ok(listaDeUsuarios, "Usuarios obtenidos correctamente.");
        }

        /// <summary>
        /// Actualiza un usuario existente. 
        /// Valida que el ID del usuario sea correcto y que el usuario exista.
        /// </summary>  
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="usuario">DTO con los nuevos datos del usuario</param>  
        /// <returns>Respuesta con el usuario actualizado y mensaje de éxito o error.</returns>
        public async Task<ServiceResponse<Usuario>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario)
        {
            ValidacionesUsuario.EsIdValido(id);
            ValidacionesUsuario.EsUsuarioValido(usuario);

            var existingUsuario = await _context.GetByIdAsync(id);

            ValidacionesUsuario.EsUsuarioExistente(existingUsuario);

            await _context.UpdateAsync(id, UsuarioMapper.ToModelActualizar(existingUsuario, usuario));

            existingUsuario = await _context.GetByIdAsync(id);

            return ServiceResponse<Usuario>.Ok(existingUsuario, "Usuario actualizado correctamente.");

        }

        /// <summary>
        /// Obtiene las tareas asociadas a un usuario específico.
        /// Valida que el ID del usuario sea correcto y que el usuario tenga tareas.    
        /// </summary>
        /// <param name="usuarioId">ID del usuario cuyas tareas se desean obtener</param>
        /// <returns>Una lista de tareas asociadas al usuario</returns>
        public async Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            ValidacionesUsuario.EsIdValido(usuarioId);

            IEnumerable<Tarea> tareasDelUsuario = await _context.ObtenerTareasPorUsuarioAsync(usuarioId);

            ValidacionesTarea.EsListaDeTareasValida(tareasDelUsuario);

            return ServiceResponse<IEnumerable<Tarea>>.Ok(tareasDelUsuario, "Tareas obtenidas correctamente.");
        }
    }
}