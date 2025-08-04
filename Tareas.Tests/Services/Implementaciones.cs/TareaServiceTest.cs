using Tareas.API.Services.Implementaciones;
using Tareas.API.DTO.Tarea;
using Moq;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Tareas.Tests.Services.Implementaciones
{
    public class TareaServiceTest
    {
        [Fact]
        public async Task CrearTareaAsync_DeberiaDevolverUnaTarea()
        {
            // Given
            var tareaACrear = new CrearTareaDTO
            {
                Titulo = "titulo test",
                Descripcion = "descripcion test",
                FechaLimite = DateTime.Now.AddDays(2)
            };

            var repositoryMock = new Mock<IRepository<Tarea>>();
            repositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Tarea>()))
                .Returns(Task.CompletedTask);

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            var resultado = await tareaService.CrearTareaAsync(tareaACrear);

            // Then
            repositoryMock.Verify(repo => repo.AddAsync(It.Is<Tarea>(t =>
                t.Titulo == tareaACrear.Titulo &&
                t.Descripcion == tareaACrear.Descripcion &&
                t.FechaLimite == tareaACrear.FechaLimite)), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal("Tarea creada exitosamente", resultado.Message);
        }

        [Fact]
        public async Task CrearTareaAsync_ConTareaNull_DeberiaDevolverUnError()
        {
            // Given
            CrearTareaDTO? tareaACrear = null;

            var repositoryMock = new Mock<IRepository<Tarea>>();
            repositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Tarea>()))
                .Returns(Task.CompletedTask);

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            ServiceResponse<ResumenTareaDTO> resultado = await tareaService.CrearTareaAsync(tareaACrear);
            // Then
            repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Tarea>()), Times.Never);
            Assert.NotNull(resultado);
            Assert.Equal("Los datos de la tarea son inválidos.", resultado.Message);
            Assert.False(resultado.Success);
        }

        [Fact]
        public async Task ObtenerTareaPorIdAsync_DeberiaDevolverUnaTarea()
        {
            // Given
            string tareaId = "507f191e810c19729de860ea";

            var repositoryMock = new Mock<IRepository<Tarea>>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2)));

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            ServiceResponse<ListarTareaDTO> resultado = await tareaService.ObtenerTareaPorIdAsync(tareaId);

            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal("Tarea obtenida exitosamente", resultado.Message);
            Assert.Equal("test tarea", resultado.Data.Titulo);
            Assert.True(resultado.Success);
        }

        [Fact]
        public async Task ObtenerTareaPorIdAsync_IdVacio_DeberiaDevolverUnError()
        {
            // Given
            string tareaId = " ";

            var repositoryMock = new Mock<IRepository<Tarea>>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2)));

            var tareaService = new TareaService(repositoryMock.Object);
            // When
            ServiceResponse<ListarTareaDTO> resultado = await tareaService.ObtenerTareaPorIdAsync(tareaId);

            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId), Times.Never);
            Assert.NotNull(resultado);
            Assert.Null(resultado.Data);
            Assert.False(resultado.Success);
        }

        
    }
}