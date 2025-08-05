using Tareas.API.Services.Implementaciones;
using Tareas.API.DTO.Tarea;
using Moq;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Models;
using Tareas.API.Repository.Implementaciones;
using System.Threading.Tasks;
using MongoDB.Bson;

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
            string idDelUsuario = "507f191e810c19729de860ea";

            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.AddAsync("507f191e810c19729de860ea", It.IsAny<Tarea>()))
                .Returns(Task.CompletedTask);

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            var resultado = await tareaService.CrearTareaAsync(idDelUsuario, tareaACrear);

            // Then
            repositoryMock.Verify(repo => repo.AddAsync("507f191e810c19729de860ea", It.Is<Tarea>(t =>
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
            string idDelUsuario = "507f191e810c19729de860ea";

            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.AddAsync(idDelUsuario, It.IsAny<Tarea>()))
                .Returns(Task.CompletedTask);

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            ServiceResponse<ResumenTareaDTO> resultado = await tareaService.CrearTareaAsync(idDelUsuario, tareaACrear);
            // Then
            repositoryMock.Verify(repo => repo.AddAsync("507f191e810c19729de860ea", It.IsAny<Tarea>()), Times.Never);
            Assert.NotNull(resultado);
            Assert.Equal("Los datos de la tarea son inválidos.", resultado.Message);
            Assert.False(resultado.Success);
        }

        [Fact]
        public async Task ObtenerTareaPorIdAsync_DeberiaDevolverUnaTarea()
        {
            // Given
            string tareaId = "507f191e810c19729de860ea";
            string idDelUsuario = "507f191e810c19729de860eb";

            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId, idDelUsuario))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2)));

            var tareaService = new TareaService(repositoryMock.Object);

            // When
            ServiceResponse<ListarTareaDTO> resultado = await tareaService.ObtenerTareaPorIdAsync(tareaId, idDelUsuario);

            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId, idDelUsuario), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal("Tarea obtenida exitosamente", resultado.Message);
            Assert.Equal("test tarea", resultado.Data.Titulo);
            Assert.True(resultado.Success);
        }

        [Fact]
        public async Task ObtenerTareaPorIdAsync_IdVacio_DeberiaDevolverUnError()
        {
            // Given
            string tareaId = "507f191e810c19729de860eb";
            string idDelUsuario = " ";


            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId, idDelUsuario))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2)));

            var tareaService = new TareaService(repositoryMock.Object);
            // When
            ServiceResponse<ListarTareaDTO> resultado = await tareaService.ObtenerTareaPorIdAsync(tareaId, idDelUsuario);

            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId, idDelUsuario), Times.Never);
            Assert.NotNull(resultado);
            Assert.Null(resultado.Data);
            Assert.False(resultado.Success);
        }

        [Fact]
        public async Task ActualizarTareaAsync_DeberiaActualizarTareaExistente()
        {
            // Given
            string tareaId = "507f191e810c19729de860ea";
            string idDelUsuario = "507f191e810c19729de860eb";

            var tareaActualizar = new ActualizarTareaDTO
            {
                Titulo = "titulo updated",
                Descripcion = "descripcion updated",
                FechaLimite = DateTime.Now.AddDays(4),
                Estado = "estado Nuevo"
            };

            var repositoryMock = new Mock<ITareaRepository>();
            var tareaExistente = new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2))
            {
                Id = ObjectId.Parse(tareaId)
            };

            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId, idDelUsuario))
                .ReturnsAsync(() => tareaExistente);

            repositoryMock
                .Setup(repo => repo.UpdateAsync(idDelUsuario, tareaActualizar, tareaId))
                .Callback<string, ActualizarTareaDTO, string>((usuarioId, tareaDto, tareaIdParam) =>
                {
                    tareaExistente.Titulo = tareaDto.Titulo;
                    tareaExistente.Descripcion = tareaDto.Descripcion;
                    tareaExistente.FechaLimite = tareaDto.FechaLimite;
                    tareaExistente.Estado = tareaDto.Estado;
                })
                .Returns(Task.CompletedTask);

            var tareaService = new TareaService(repositoryMock.Object);
            // When
            ServiceResponse<ResumenTareaDTO> resultado = await tareaService.ActualizarTareaAsync(idDelUsuario, tareaId, tareaActualizar);
            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId, idDelUsuario), Times.Exactly(2));
            Assert.True(resultado.Success);
            Assert.Equal("Tarea actualizada exitosamente", resultado.Message);
            Assert.Equal(tareaActualizar.Titulo, resultado.Data.Titulo);
            Assert.Equal(tareaActualizar.FechaLimite, resultado.Data.FechaLimite);
            Assert.Equal(tareaActualizar.Estado, resultado.Data.Estado);
        }

        [Fact]
        public async Task ActualizarTareaAsync_ConIdDelUsuarioNull_DeberiaDevolverError()
        {
            // Given
            string tareaId = "507f191e810c19729de860ea";
            string? idDelUsuario = null;

            var tareaActualizar = new ActualizarTareaDTO
            {
                Titulo = "updated titulo",
                Descripcion = "descripcion updated",
                FechaLimite = DateTime.Now.AddDays(4),
                Estado = "estado Nuevo"
            };

            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId, idDelUsuario))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2))
                {
                    Id = ObjectId.Parse(tareaId)
                });

            var tareaService = new TareaService(repositoryMock.Object);
            // When
            ServiceResponse<ResumenTareaDTO> resultado = await tareaService.ActualizarTareaAsync(idDelUsuario, tareaId, tareaActualizar);
            // Then
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId, idDelUsuario), Times.Never);
            Assert.False(resultado.Success);
            Assert.Equal("El ID del  Usuario es inválido.", resultado.Message);
        }

        [Fact]
        public async Task EliminarTareaAsync_DeberiaEliminarLaTarea()
        {
            // Given
            string tareaId = "507f191e810c19729de860ea";
            string idDelUsuario = "507f191e810c19729de860eb";

            var repositoryMock = new Mock<ITareaRepository>();
            repositoryMock
                .Setup(repo => repo.GetByIdAsync(tareaId, idDelUsuario))
                .ReturnsAsync(new Tarea("test tarea", "desc test", "estado", DateTime.Now.AddDays(2))
                {
                    Id = ObjectId.Parse(tareaId)
                });

            var tareaService = new TareaService(repositoryMock.Object);
            // When
            ServiceResponse<ResumenTareaDTO> resultado = await tareaService.EliminarTareaAsync(tareaId, idDelUsuario);
            // Then
            Assert.True(resultado.Success);
            repositoryMock.Verify(repo => repo.GetByIdAsync(tareaId, idDelUsuario), Times.AtMostOnce());
            repositoryMock.Verify(repo => repo.DeleteAsync(idDelUsuario, tareaId), Times.Once);
        }
    }
}