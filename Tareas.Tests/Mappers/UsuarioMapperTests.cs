using Tareas.API.DTO.Usuario;
using Tareas.API.Models;
using Tareas.API.Mappers;
using Tareas.API.Services.Helpers;

namespace Tareas.Tests.Mappers
{
    public class UsuarioMapperTests
    {
        [Fact]
        public void ToModel_DeberiaCrearUnUsuario()
        {
            // Arrange
            string contrasena = "password123";
            var dto = new CrearUsuarioDTO
            {
                Nombre = "Test Usuario",
                Email = "test@gmail.com",
                Contrasena = contrasena
            };
            // Act
            Usuario usuario = UsuarioMapper.ToModel(dto);

            // Assert
            Assert.NotNull(usuario);
            Assert.Equal(dto.Nombre, usuario.Nombre);
            Assert.Equal(dto.Email, usuario.Email);
            Assert.Equal(CalculoHash.GenerarHash(contrasena), usuario.PasswordHash);
        }

        [Fact]
        public void ToListarDTO_DeberiaConvertirUsuarioADetalleUsuarioDTO()
        {
            // Arrange
            var usuario = new Usuario("Test Usuario", "emailfalso@gmail.com", CalculoHash.GenerarHash("password123"))
            {
                Tareas = new List<Tarea> { new Tarea("titulo", "desc", "pendiente", DateTime.Now.AddDays(2)), new Tarea("titulo", "desc", "pendiente", DateTime.Now.AddDays(2)) } // Simulando dos tareas
            };

            // Act
            DetalleUsuarioDTO detalleUsuarioDTO = UsuarioMapper.ToListarDTO(usuario);

            // Assert
            Assert.NotNull(detalleUsuarioDTO);
            Assert.Equal(usuario.Nombre, detalleUsuarioDTO.Nombre);
            Assert.Equal(usuario.Email, detalleUsuarioDTO.Email);
            Assert.Equal(usuario.FechaRegistro.ToString("yyyy-MM-dd"), detalleUsuarioDTO.FechaCreacion);
            Assert.Equal(usuario.Tareas.Count, detalleUsuarioDTO.CantidadTareas);
        }

        [Fact]
        public void ToResumenDTO_DeberiaConvertirUsuarioAResumenDTO()
        {
            // Arrange
            var usuario = new Usuario("Test Usuario", "emailfalso@gmail.com", CalculoHash.GenerarHash("password123"));
            // Act
            ResumenUsuarioDTO resumenUsuarioDTO = UsuarioMapper.ToResumenDTO(usuario);
            // Then
            Assert.NotNull(resumenUsuarioDTO);
            Assert.Equal(usuario.Nombre, resumenUsuarioDTO.Nombre);
            Assert.Equal(usuario.Email, resumenUsuarioDTO.Email);
        }
    }
}