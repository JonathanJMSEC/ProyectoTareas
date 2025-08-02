using Tareas.API.DTO.Usuario;
using Tareas.API.Services.Helpers;
using Tareas.API.Models;

namespace Tareas.API.Mappers
{
    /// <summary>
    /// Mapper para convertir entre DTOs y modelos de Usuario.
    /// /// </summary>
    public static class UsuarioMapper
    {
        /// <summary>
        /// Convierte un DTO de creación de usuario a un modelo de Usuario.
        /// /// </summary>
        /// <param name="dto">DTO con los datos del usuario a crear.</param>
        /// <returns>Modelo de Usuario con los datos del DTO.</returns>
        public static Usuario ToModel(CrearUsuarioDTO dto)
        {
            if (dto == null)
                return null;

            string contraseñaHasheada = CalculoHash.GenerarHash(dto.Contrasena);

            return new Usuario(dto.Nombre, dto.Email, contraseñaHasheada);
        }

        /// <summary>
        /// Convierte un modelo de Usuario a un DTO para listar usuarios.
        /// /// </summary>
        /// <param name="usuario">Modelo de Usuario a convertir.</param>    
        /// <returns>DTO DetalleUsuarioDTO con los datos del usuario.</returns>
        public static DetalleUsuarioDTO ToListarDTO(Usuario usuario)
        {
            if (usuario == null)
                return null;

            return new DetalleUsuarioDTO
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaRegistro.ToString("yyyy-MM-dd"),
                CantidadTareas = usuario.Tareas.Count
            };
        }

        /// <summary>
        /// Convierte un modelo de Usuario a un DTO para resumen de usuario.   
        /// /// </summary>
        /// <param name="usuario">Modelo de Usuario a convertir.</param>
        /// <returns>ResumenUsuarioDTO con los datos resumidos del usuario.</returns>
        public static ResumenUsuarioDTO ToResumenDTO(Usuario usuario)
        {
            if (usuario == null)
                return null;

            return new ResumenUsuarioDTO
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };
        }

        /// <summary>
        /// Actualiza un modelo de Usuario existente con los datos de un DTO de actualización.
        /// </summary>
        /// <param name="existingUsuario">Modelo de Usuario existente a actualizar.</param>
        /// <param name="dtoConDatosActualizados">DTO con los nuevos datos del usuario.</param>
        /// <returns>Modelo de Usuario actualizado con los datos del DTO.</returns>
        public static Usuario ToModelActualizar(Usuario existingUsuario, CrearUsuarioDTO dtoConDatosActualizados)
        {
            if (existingUsuario == null || dtoConDatosActualizados == null)
                return existingUsuario;

            existingUsuario.Nombre = dtoConDatosActualizados.Nombre;
            existingUsuario.Email = dtoConDatosActualizados.Email;

            if (!string.IsNullOrEmpty(dtoConDatosActualizados.Contrasena))
            {
                existingUsuario.ActualizarPassword(CalculoHash.GenerarHash(dtoConDatosActualizados.Contrasena));
            }

            return existingUsuario;
        }
    }
}