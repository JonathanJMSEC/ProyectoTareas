using Tareas.API.DTO.Tarea;
using Tareas.API.Models;
using Tareas.API.Mappers;
using MongoDB.Bson;

namespace Tareas.Tests.Mappers
{
    public class TareaMapperTests
    {
        [Fact]
        public void ToModel_DeberiaCrearUnaTarea()
        {
            //Arrange
            var dto = new CrearTareaDTO
            {
                Titulo = "Test Tarea",
                Descripcion = "Descripción de prueba",
                FechaLimite = DateTime.Now.AddDays(1)
            };

            //ACT
            Tarea tarea = TareaMapper.ToModel(dto);

            //ASSERT
            Assert.NotNull(tarea);
        }

        [Fact]
        public void ToListarDTO_DeberiaConvertirTareaAModeloListar()
        {
            //  Arrange 
            Tarea tarea = new Tarea("Test Tarea", "Descripción de prueba", "Pendiente", DateTime.Now.AddDays(1));

            // Act
            ListarTareaDTO dto = TareaMapper.ToListarDTO(tarea);

            // Assert
            Assert.NotNull(dto);
        }

        [Fact]
        public void ToResumenDTO_DeberiaConvertirTareaAModeloResumen()
        {
            // Arrange
            Tarea tarea = new Tarea("Test Tarea", "Descripción de prueba", "Pendiente", DateTime.Now.AddDays(1));

            // Act
            ResumenTareaDTO dto = TareaMapper.ToResumenDTO(tarea);

            // Assert
            Assert.NotNull(dto);
        }

        [Fact]
        public void ToModel_ActualizarTareaDTO_DeberiaActualizarTareaExistente()
        {
            // Arrange
            Tarea existente = new Tarea("Tarea Existente", "Descripción existente", "Pendiente", DateTime.Now.AddDays(1))
            {
                Id = ObjectId.GenerateNewId(),
                FechaCreacion = DateTime.Now
            };

            ActualizarTareaDTO dto = new ActualizarTareaDTO
            {
                Titulo = "Tarea Actualizada",
                Descripcion = "Descripción actualizada",
                Estado = "Completada",
                FechaLimite = DateTime.Now.AddDays(2)
            };

            // Act
            Tarea tareaActualizada = TareaMapper.ToModel(existente, dto);

            // Assert
            Assert.NotNull(tareaActualizada);
            Assert.Equal(dto.Titulo, tareaActualizada.Titulo);
            Assert.Equal(dto.Descripcion, tareaActualizada.Descripcion);
            Assert.Equal(dto.Estado, tareaActualizada.Estado);
            Assert.Equal(dto.FechaLimite, tareaActualizada.FechaLimite);
            Assert.Equal(existente.Id, tareaActualizada.Id); // Verificar que el ID se preserva
            Assert.Equal(existente.FechaCreacion, tareaActualizada.FechaCreacion); // Verificar que la fecha de creación se preserva
        }
    }
}