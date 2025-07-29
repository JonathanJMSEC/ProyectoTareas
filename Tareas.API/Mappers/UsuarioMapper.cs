using DTO.Usuario;
using Services.Helpers;
using Tareas.API.Models;

namespace Tareas.API.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario ToModel(CrearUsuarioDTO dto)
        {
            if (dto == null)
                return null;

            string contraseñaHasheada = CalculoHash.GenerarHash(dto.Contrasena);

            return new Usuario(dto.Nombre, dto.Email, contraseñaHasheada);
        }

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
    }
}