using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.Metrics;
using University.Core.CustomEntities;
using University.Core.Entities;
using University.Core.Exceptions;
using University.Core.Interfaces;
using University.Core.QueryFilters;
using University.Core.Services;

namespace University.Test.ServicesTests
{
    public class StudentServiceTests
    {
        private readonly PaginationOptions _paginationOptions;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IOptions<PaginationOptions>> mockOptions;
        private readonly EstudianteService _estudianteService;

        public StudentServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockOptions = new Mock<IOptions<PaginationOptions>>();
            _paginationOptions = new PaginationOptions();
            _estudianteService = new EstudianteService(mockUnitOfWork.Object, mockOptions.Object);
            mockOptions.Setup(o => o.Value).Returns(_paginationOptions);
        }

        [Fact]
        public async Task CreateStudent_ReturnStudent()
        {
            // Arrange
            int studentId = 1;
            Estudiante expectedStudent = new Estudiante { Id = 1, Nombre = "David", CiudadId = 1 };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(studentId)).ReturnsAsync(expectedStudent);

            //// Act
            Estudiante? result = await _estudianteService.GetEstudiante(studentId);

            //// Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStudent, result);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetById(studentId), Times.Once);
        }
        [Fact]
        public async Task CreateStudent_ReturnEstudianteNoRegistrado()
        {
            // Arrange
            int studentId = 1;
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(studentId)).ReturnsAsync((Estudiante?)null);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _estudianteService.GetEstudiante(studentId);
            });
            Assert.Equal("El estudiante no está registrado", exception.Message);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetById(studentId), Times.Once);
        }
        [Fact]
        public async Task GetStudents_ReturnPagedListStudents()
        {
            // Arrange
            QueryFilter filters = new QueryFilter
            {
                PageNumber = 1,
                PageSize = 2
            };
            List<Estudiante> clients = new List<Estudiante>
            {
                new Estudiante { Id = 1,Nombre = "Estudiante 1", CiudadId = 1 },
                new Estudiante { Id = 2,Nombre = "Estudiante 2", CiudadId = 2 },
                new Estudiante { Id = 3,Nombre = "Estudiante 3", CiudadId = 3 },
                new Estudiante { Id = 4,Nombre = "Estudiante 4", CiudadId = 4 },
            };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetAll()).ReturnsAsync(clients);

            // Act
            PagedList<Estudiante> result = await _estudianteService.GetEstudiantes(filters);

            // Assert
            Assert.NotNull(result);
            for (int i = 0; i < 2; i++)
                Assert.Equal(clients[i], result[i]);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetAll(), Times.Once);
        }
        [Fact]
        public async Task GetStudents_ReturnNoHayEstudiantesRegistrados()
        {
            // Arrange
            QueryFilter filters = new QueryFilter
            {
                PageNumber = 1,
                PageSize = 2
            };
            List<Estudiante> estudiantes = new List<Estudiante>();
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetAll()).ReturnsAsync(estudiantes);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _estudianteService.GetEstudiantes(filters);
            });
            Assert.Equal("Aún no hay estudiantes registrados", exception.Message);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetAll(), Times.Once);
        }

        [Fact]
        public async Task InsertStudent_ReturnTrue()
        {
            // Arrange
            Estudiante estudiante = new Estudiante { Id = 1, Nombre = "Estudiante 1", CiudadId = 1 };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.Add(estudiante)).Verifiable();
            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Verifiable();

            // Act
            bool response = await _estudianteService.InsertEstudiante(estudiante);

            // Assert
            Assert.True(response);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.Add(estudiante), Times.Once);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateStudent_ReturnTrue()
        {
            // Arrange
            Estudiante estudiante = new Estudiante { Id = 1, Nombre = "Estudiante 1", CiudadId = 1 };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(1)).ReturnsAsync(estudiante);
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.Update(estudiante)).Verifiable();
            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Verifiable();

            // Act
            bool result = await _estudianteService.UpdateEstudiante(estudiante);

            // Assert
            Assert.True(result);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetById(1), Times.Once);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.Update(estudiante), Times.Once);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateStudent_ReturnEstudianteNoRegistrado()
        {
            // Arrange
            Estudiante estudiante = new Estudiante { Id = 1, Nombre = "Estudiante 1", CiudadId = 1 };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(estudiante.Id)).ReturnsAsync((Estudiante?)null);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _estudianteService.UpdateEstudiante(estudiante);
            });
            Assert.Equal("El estudiante no está registrado", exception.Message);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetById(estudiante.Id), Times.Once);
        }
        [Fact]
        public async Task DeleteClient_ReturnTrue()
        {
            // Arrange
            int studentId = 1;
            Estudiante returnEstudiante = new Estudiante { Id = 1, Nombre = "Estudiante 1", CiudadId = 1 };
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(studentId)).ReturnsAsync(returnEstudiante);
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.Delete(studentId)).Verifiable();
            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Verifiable();

            // Act
            bool result = await _estudianteService.DeleteEstudiante(studentId);

            // Assert
            Assert.True(result);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.GetById(studentId), Times.Once);
            mockUnitOfWork.Verify(uow => uow.EstudianteRepository.Delete(studentId), Times.Once);
            mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteStudent_ReturnElClienteNoEstáRegistrada()
        {
            // Arrange
            int id = 1;
            mockUnitOfWork.Setup(uow => uow.EstudianteRepository.GetById(id)).ReturnsAsync((Estudiante?)null);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _estudianteService.DeleteEstudiante(id);
            });
            Assert.Equal("El estudiante no está registrado", exception.Message);
        }
    }
}
