using Tareas.API.DTO.Usuario;
using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Helpers
{
    /// <summary>
    /// Clase para validar usuarios.
    /// </summary>
    public static class ValidacionesUsuario
    {
        /// <summary>
        /// Valida si un usuario es válido para crear.
        /// </summary>
        /// <param name="usuario">DTO de usuario a validar.</param>
        /// <returns>ServiceResponse con el resultado de la validación.</returns>
        public static ServiceResponse<CrearUsuarioDTO> EsUsuarioValido(CrearUsuarioDTO usuario)
        {
            if (usuario == null)
                return ServiceResponse<CrearUsuarioDTO>.Error("El usuario no puede ser nulo.");
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                return ServiceResponse<CrearUsuarioDTO>.Error("El nombre del usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
                return ServiceResponse<CrearUsuarioDTO>.Error("El email del usuario es obligatorio y debe ser válido.");
            if (string.IsNullOrWhiteSpace(usuario.Contrasena) || usuario.Contrasena.Length < 6)
                return ServiceResponse<CrearUsuarioDTO>.Error("La contraseña del usuario es obligatoria y debe tener al menos 6 caracteres.");

            return ServiceResponse<CrearUsuarioDTO>.Ok(usuario, "El usuario es válido.");
        }

        /// <summary>
        /// valida si  un  id es válido.
        /// /// </summary>
        /// <param name="id">ID del usuario a validar.</param>
        /// <returns>ServiceResponse con el resultado de la validación.</returns>
        public static ServiceResponse<Tarea> EsIdValido(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea no puede ser nulo o vacío.");

            return ServiceResponse<Tarea>.Ok(null, "El ID es válido.");
        }

        /// <summary>
        /// Valida si un usuario existe.
        /// </summary>
        /// <param name="usuario">Modelo de usuario a validar.</param>
        /// <returns>ServiceResponse con el resultado de la validación.</returns>
        public static ServiceResponse<Usuario> EsUsuarioExistente(Usuario usuario)
        {
            if (usuario == null)
                return ServiceResponse<Usuario>.Error("El usuario no existe.");

            return ServiceResponse<Usuario>.Ok(usuario, "El usuario existe.");
        }

        /// <summary>
        /// Valida si una lista de usuarios es válida.
        /// /// </summary>
        /// <param name="usuarios">Lista de usuarios a validar.</param>
        /// <returns>ServiceResponse con el resultado de la validación.</returns>
        public static ServiceResponse<IEnumerable<Usuario>> EsListaDeUsuariosValida(IEnumerable<Usuario> usuarios)
        {
            if (usuarios == null || !usuarios.Any())
                return ServiceResponse<IEnumerable<Usuario>>.Error("La lista de usuarios no puede ser nula o vacía.");

            return ServiceResponse<IEnumerable<Usuario>>.Ok(usuarios, "La lista de usuarios es válida.");
        }
    }
}