using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Api.Controllers;
using University.Api.Responses;
using University.Core.DTOs;
using University.Core.Entities;
using University.Core.Interfaces;

namespace University.Test.Helpers
{
    public class StudyControllerTests
    {
        private readonly Mock<IEstudianteService> mockStudentService;
        private readonly Mock<IMapper> mapperMock;
        private readonly EstudianteController controller;
        private readonly EstudianteDto studentDto;
        private readonly Estudiante student;

        public StudyControllerTests()
        {
            mockStudentService = new Mock<IEstudianteService>();
            mapperMock = new Mock<IMapper>();
            controller = new EstudianteController(mockStudentService.Object, mapperMock.Object);
            studentDto = new EstudianteDto { Nombre = "Test Country", CiudadId = 1 };
            student = new Estudiante { Id = 1, Nombre = "Test Country", CiudadId = 1 };
        }
        [Fact]
        public async Task GetStudnet_ReturnsStudyDto()
        {
            ApiResponse<EstudianteDto> expectedApiResponse = new ApiResponse<EstudianteDto>(studentDto);
            mockStudentService.Setup(s => s.GetEstudiante(1)).ReturnsAsync(student);
            mapperMock.Setup(m => m.Map<EstudianteDto>(student)).Returns(studentDto);

            IActionResult actionResult = await controller.GetEstudiante(1);
            OkObjectResult okResult = (OkObjectResult)actionResult;
            ApiResponse<EstudianteDto> returnedApiResponse = Assert.IsType<ApiResponse<EstudianteDto>>(okResult.Value);

            mockStudentService.Verify(service => service.GetEstudiante(1), Times.Once);
            ControllerTestsHelpers.checkResponseApi(okResult, returnedApiResponse, expectedApiResponse);
        }
    }
}
