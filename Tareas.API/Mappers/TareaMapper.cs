using Tareas.API.DTO.Tarea;
using Tareas.API.Models;

namespace Tareas.API.Mappers
{
    public static class TareaMapper
    {
        public static Tarea ToModel(CrearTareaDTO dto)
        {
            if (dto == null)
                return null;

            return new Tarea(dto.Titulo, dto.Descripcion, "Pendiente", dto.FechaLimite);
        }

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

        public  static Tarea ToModel(Tarea existente, ActualizarTareaDTO dto)
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