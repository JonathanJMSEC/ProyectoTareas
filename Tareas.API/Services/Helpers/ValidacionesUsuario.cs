using Tareas.API.DTO.Usuario;
using Tareas.API.Models;

namespace Tareas.API.Services.Helpers
{
    public static class ValidacionesUsuario
    {
        public static bool EsUsuarioValido(CrearUsuarioDTO usuario)
        {
            return usuario != null &&
                   !string.IsNullOrWhiteSpace(usuario.Nombre) &&
                   !string.IsNullOrWhiteSpace(usuario.Email) &&
                   !string.IsNullOrWhiteSpace(usuario.Contrasena);
        }

        public static bool EsIdValido(string id)
        {
            return !string.IsNullOrWhiteSpace(id);
        }

        public static bool EsUsuarioExistente(Usuario usuario)
        {
            return usuario != null;
        }

        public static bool EsListaDeUsuariosValida(IEnumerable<Usuario> usuarios)
        {
            return usuarios != null && usuarios.Any();
        }
    }
}