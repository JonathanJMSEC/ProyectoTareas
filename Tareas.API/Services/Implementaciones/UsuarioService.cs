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
        public async Task<ServiceResponse<ResumenUsuarioDTO>> CrearUsuarioAsync(CrearUsuarioDTO usuario)
        {
            if (!ValidacionesUsuario.EsUsuarioValido(usuario).Success)
                return ServiceResponse<ResumenUsuarioDTO>.Error("Los datos del usuario son inválidos.");

            var usuarioCreado = UsuarioMapper.ToModel(usuario);

            await _context.AddAsync(usuarioCreado);

            return ServiceResponse<ResumenUsuarioDTO>.Ok(UsuarioMapper.ToResumenDTO(usuarioCreado), "El usuario ha sido creado correctamente.");
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// Valida que el ID sea correcto y que el usuario exista.
        /// </summary>
        /// <returns>Respuesta con el usuario obtenido y mensaje de éxito o error.</returns>    
        public async Task<ServiceResponse<ResumenUsuarioDTO>> ObtenerUsuarioPorIdAsync(string id)
        {
            if (!ValidacionesUsuario.EsIdValido(id).Success)
                return ServiceResponse<ResumenUsuarioDTO>.Error("El ID del usuario es inválido.");

            var usuarioObtenido = await _context.GetByIdAsync(id);

            if (!ValidacionesUsuario.EsUsuarioExistente(usuarioObtenido).Success)
                return ServiceResponse<ResumenUsuarioDTO>.Error("El usuario no existe.");

            return ServiceResponse<ResumenUsuarioDTO>.Ok(UsuarioMapper.ToResumenDTO(usuarioObtenido), "Usuario obtenido correctamente.");

        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema. 
        /// Valida que la lista de usuarios no esté vacía.
        /// </summary>
        /// <returns>Una lista de usuarios</returns>
        public async Task<ServiceResponse<IEnumerable<Usuario>>> ObtenerTodosLosUsuariosAsync()
        {
            IEnumerable<Usuario> listaDeUsuarios = await _context.GetAllAsync();

            if (!ValidacionesUsuario.EsListaDeUsuariosValida(listaDeUsuarios).Success)
                return ServiceResponse<IEnumerable<Usuario>>.Error("No se encontraron usuarios.");

            return ServiceResponse<IEnumerable<Usuario>>.Ok(listaDeUsuarios, "Usuarios obtenidos correctamente.");
        }

        /// <summary>
        /// Actualiza un usuario existente. 
        /// Valida que el ID del usuario sea correcto y que el usuario exista.
        /// </summary>  
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="usuario">DTO con los nuevos datos del usuario</param>  
        /// <returns>Respuesta con el usuario actualizado y mensaje de éxito o error.</returns>
        public async Task<ServiceResponse<ResumenUsuarioDTO>> ActualizarUsuarioAsync(string id, CrearUsuarioDTO usuario)
        {
            var existingUsuario = await _context.GetByIdAsync(id);

            if (ValidacionesUsuario.EsIdValido(id).Success == false ||  ValidacionesUsuario.EsUsuarioValido(usuario).Success == false || existingUsuario == null)
                return ServiceResponse<ResumenUsuarioDTO>.Error("El usuario no existe o los datos proporcionados son inválidos.");

            await _context.UpdateAsync(id, UsuarioMapper.ToModelActualizar(existingUsuario, usuario));

            existingUsuario = await _context.GetByIdAsync(id);

            return ServiceResponse<ResumenUsuarioDTO>.Ok(UsuarioMapper.ToResumenDTO(existingUsuario), "Usuario actualizado correctamente.");

        }

        /// <summary>
        /// Obtiene las tareas asociadas a un usuario específico.
        /// Valida que el ID del usuario sea correcto y que el usuario tenga tareas.    
        /// </summary>
        /// <param name="usuarioId">ID del usuario cuyas tareas se desean obtener</param>
        /// <returns>Una lista de tareas asociadas al usuario</returns>
        public async Task<ServiceResponse<IEnumerable<Tarea>>> ObtenerTareasPorUsuarioAsync(string usuarioId)
        {
            if (!ValidacionesUsuario.EsIdValido(usuarioId).Success)
                return ServiceResponse<IEnumerable<Tarea>>.Error("El ID del usuario es inválido.");

            IEnumerable<Tarea> tareasDelUsuario = await _context.ObtenerTareasPorUsuarioAsync(usuarioId);

            if  (!ValidacionesTarea.EsListaDeTareasValida(tareasDelUsuario).Success)
                return ServiceResponse<IEnumerable<Tarea>>.Error("El usuario no tiene tareas asociadas.");

            return ServiceResponse<IEnumerable<Tarea>>.Ok(tareasDelUsuario, "Tareas obtenidas correctamente.");
        }
    }
}