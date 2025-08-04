using Tareas.API.Services.Implementaciones;
using Tareas.API.DTO.Usuario;
using Moq;
using Tareas.API.Repository.Interfaces;
using Tareas.API.Models;
using MongoDB.Bson;

namespace Tareas.Tests.Services.Implementaciones
{
    public class UsuarioServiceTest
    {
        [Fact]
        public async Task CrearUsuarioAsync_DeberiaDevolverUnUsuario()
        {
            // Arrange
            var usuarioDto = new CrearUsuarioDTO
            {
                Nombre = "Test User",
                Email = "test@gmail.com",
                Contrasena = "TestPassword123"
            };

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.CrearUsuarioAsync(usuarioDto);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Usuario>(u =>
                u.Nombre == usuarioDto.Nombre &&
                u.Email == usuarioDto.Email)), Times.Once);

            Assert.NotNull(resultado);
            Assert.Equal(usuarioDto.Nombre, resultado.Data.Nombre);
            Assert.Equal(usuarioDto.Email, resultado.Data.Email);
            Assert.Equal("El usuario ha sido creado correctamente.", resultado.Message);
            Assert.True(resultado.Success);
        }

        [Fact]
        public async Task CrearUsuarioAsync_UsuarioNull_DeberiaLanzarMensajeDeError()
        {
            // Arrange
            CrearUsuarioDTO? usuarioDto = null;

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Usuario>()))
                .Returns(Task.CompletedTask);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.CrearUsuarioAsync(usuarioDto);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Usuario>()), Times.Never);
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
        }

        [Fact]
        public async Task ObtenerUsuarioPorIdAsync_DeberiaDevolverUnUsuario()
        {
            // Arrange
            var usuarioId = "507f191e810c19729de860ea";

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(usuarioId))
                .ReturnsAsync(new Usuario("Test User", "emialtest@email.com", "TestPassword123")
                {
                    Id = ObjectId.Parse(usuarioId)
                });
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ObtenerUsuarioPorIdAsync(usuarioId);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.GetByIdAsync(usuarioId), Times.Once);


            Assert.NotNull(resultado);
            Assert.Equal("Usuario obtenido correctamente.", resultado.Message);
            Assert.True(resultado.Success);
        }

        [Fact]
        public async Task ObtenerUsuarioPorIdAsync_UsuarioNoExistente_DeberiaDevolverUnError()
        {
            // Arrange
            string? usuarioId = null;

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(usuarioId))
                .ThrowsAsync(new Exception("Usuario no encontrado"));
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ObtenerUsuarioPorIdAsync(usuarioId);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.GetByIdAsync(usuarioId), Times.Never);
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal("El ID del usuario es inválido.", resultado.Message);
        }

        [Fact]
        public async Task ObtenerTodosLosUsuariosAsync_DeberiaDevolverUnaListaDeUsuarios()
        {
            // Arrange
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Usuario>
                {
                    new Usuario("User1", "User2", "Password1"),
                    new Usuario("User2", "User2", "Password2")
                });
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ObtenerTodosLosUsuariosAsync();

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);

            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<IEnumerable<Usuario>>(resultado.Data);
            Assert.Equal(2, resultado.Data.Count());
            Assert.Equal("Usuarios obtenidos correctamente.", resultado.Message);
        }

        [Fact]
        public async Task ActualizarUsuarioAsync_DeberiaActualizarUnUsuarioExistente()
        {
            // Arrange
            var usuarioId = "507f191e810c19729de860ea";
            var usuarioDto = new CrearUsuarioDTO
            {
                Nombre = "Updated User",
                Email = "updatedEmail@emial.com",
                Contrasena = "UpdatedPassword123"
            };

            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(usuarioId))
                .ReturnsAsync(new Usuario("Old User", "oldEmail@email.com", "OldPassword123")
                {
                    Id = ObjectId.Parse(usuarioId)
                });
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ActualizarUsuarioAsync(usuarioId, usuarioDto);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.GetByIdAsync(usuarioId), Times.Exactly(2));

            Assert.NotNull(resultado);
            Assert.Equal("Usuario actualizado correctamente.", resultado.Message);
            Assert.True(resultado.Success);
            Assert.Equal(usuarioDto.Nombre, resultado.Data.Nombre);
            Assert.Equal(usuarioDto.Email, resultado.Data.Email);
        }

        [Fact]
        public async Task ActualizarUsuarioAsync_UsuarioSinNombre_DeberiaDevolverUnError()
        {
            // Arrange
            var usuarioId = "507f191e810c19729de860ea";
            var usuarioDto = new CrearUsuarioDTO
            {
                Nombre = null,
                Email = "test@",
                Contrasena = "12345"
            };
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.GetByIdAsync(usuarioId))
                .ReturnsAsync(new Usuario("Old User", "old@mail", "oldPassword")
                {
                    Id = ObjectId.Parse(usuarioId)
                });
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ActualizarUsuarioAsync(usuarioId, usuarioDto);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.GetByIdAsync(usuarioId), Times.Once);
            usuarioRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<string>(), It.IsAny<Usuario>()), Times.Never);
            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
        }

        [Fact]
        public async Task ObtenerTareasPorUsuarioAsync_DeberiaDevolverLasTareasDelUsuario()
        {
            // Arrange
            string usuarioId = "12345";
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            usuarioRepositoryMock
                .Setup(repo => repo.ObtenerTareasPorUsuarioAsync(usuarioId))
                .ReturnsAsync(new List<Tarea>
                {
                    new Tarea("titulo", "desc", "estado", DateTime.Now.AddDays(1)),
                    new Tarea("titulo2", "desc2", "estado2", DateTime.Now.AddDays(2))
                });
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);

            // Act
            var resultado = await usuarioService.ObtenerTareasPorUsuarioAsync(usuarioId);

            // Assert
            usuarioRepositoryMock.Verify(repo => repo.ObtenerTareasPorUsuarioAsync(usuarioId), Times.Once);

            Assert.NotNull(resultado);
            Assert.Equal("Tareas obtenidas correctamente.", resultado.Message);
            Assert.IsAssignableFrom<IEnumerable<Tarea>>(resultado.Data);
        }
    }
}