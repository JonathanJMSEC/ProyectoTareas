using Tareas.API.DTO.Usuario;
using Tareas.API.Models;
using Tareas.API.Services.Implementaciones;

namespace Tareas.API.Services.Helpers
{
    public static class ValidacionesUsuario
    {
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

        public static ServiceResponse<Tarea> EsIdValido(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ServiceResponse<Tarea>.Error("El ID de la tarea no puede ser nulo o vacío.");

            return ServiceResponse<Tarea>.Ok(null, "El ID es válido.");
        }

        public static ServiceResponse<Usuario> EsUsuarioExistente(Usuario usuario)
        {
            if (usuario == null)
                return ServiceResponse<Usuario>.Error("El usuario no existe.");

            return ServiceResponse<Usuario>.Ok(usuario, "El usuario existe.");
        }

        public static  ServiceResponse<IEnumerable<Usuario>> EsListaDeUsuariosValida(IEnumerable<Usuario> usuarios)
        {
            if (usuarios == null || !usuarios.Any())
                return ServiceResponse<IEnumerable<Usuario>>.Error("La lista de usuarios no puede ser nula o vacía.");

            return ServiceResponse<IEnumerable<Usuario>>.Ok(usuarios, "La lista de usuarios es válida.");
        }
    }
}