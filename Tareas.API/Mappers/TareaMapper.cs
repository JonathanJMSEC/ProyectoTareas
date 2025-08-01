using Tareas.API.DTO.Tarea;
using Tareas.API.Models;

namespace Tareas.API.Mappers
{
    /// <summary>
    /// Mapper para convertir entre DTOs y modelos de Tarea.
    /// /// </summary>
    public static class TareaMapper
    {
        /// <summary>
        /// Convierte un DTO de creación de tarea a un modelo de Tarea.
        /// /// </summary>
        /// <param name="dto">DTO con los datos de la tarea a crear.</param>
        /// <returns>Modelo de Tarea con los datos del DTO.</returns>
        public static Tarea ToModel(CrearTareaDTO dto)
        {
            if (dto == null)
                return null;

            return new Tarea(dto.Titulo, dto.Descripcion, "Pendiente", dto.FechaLimite);
        }

        /// <summary>
        /// Convierte un modelo de Tarea a un DTO para listar tareas.
        /// </summary>
        /// <param name="tarea">Modelo de Tarea a convertir.</param>
        /// <returns>DTO ListarTareaDTO con los datos de la tarea.</returns>
        public static ListarTareaDTO ToListarDTO(Tarea tarea)
        {
            if (tarea == null)
                return null;

            return new ListarTareaDTO
            {
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaLimite = tarea.FechaLimite,
                Estado = tarea.Estado,
                FechaCreacion = tarea.FechaCreacion
            };
        }

        /// <summary>
        /// Convierte un modelo de Tarea a un DTO para resumen de tarea.
        /// </summary>
        /// <param name="tarea">Modelo de Tarea a convertir.</param>
        /// <returns>ResumenTareaDTO con los datos resumidos de la tarea.</returns>
        public static ResumenTareaDTO ToResumenDTO(Tarea tarea)
        {
            if (tarea == null)
                return null;

            return new ResumenTareaDTO
            {
                Titulo = tarea.Titulo,
                FechaLimite = tarea.FechaLimite,
                Estado = tarea.Estado
            };
        }

        /// <summary>
        /// Actualiza un modelo de Tarea existente con los datos de un DTO de actualización.
        /// </summary>
        /// <param name="existente">Modelo de Tarea existente a actualizar.</param>
        /// <param name="dto">DTO con los nuevos datos de la tarea.</param>
        /// <returns>Modelo de Tarea actualizado con los datos del DTO.</returns>
        public static Tarea ToModel(Tarea existente, ActualizarTareaDTO dto)
        {
            if (dto == null)
                return null;

            return new Tarea(dto.Titulo, dto.Descripcion, dto.Estado, dto.FechaLimite)
            {
                Id = existente.Id, // Preservar el ID existente
                FechaCreacion = existente.FechaCreacion // Preservar la fecha de creación
            };
        }
    }
    

}