using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using University.Api.Responses;
using University.Core.CustomEntities;
using University.Core.DTOs;
using University.Core.Entities;
using University.Core.Interfaces;
using University.Core.QueryFilters;

namespace University.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IMapper _mapper;

        public EstudianteController(IEstudianteService estudianteService, IMapper mapper)
        {
            _estudianteService = estudianteService;
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieve all students
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet("GetEstudiantes")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EstudianteDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEstudiantes([FromQuery] QueryFilter filters)
        {
            PagedList<Estudiante> estudiantes = await _estudianteService.GetEstudiantes(filters);
            IEnumerable<EstudianteDto> estudianteDto = _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
            Metadata metadata = new Metadata
            {
                TotalCount = estudiantes.TotalCount,
                PageSize = estudiantes.PageSize,
                CurrentPage = estudiantes.CurrentPage,
                TotalPages = estudiantes.TotalPages,
                HasNextPage = estudiantes.HasNextPage,
                HasPreviousPage = estudiantes.HasPreviousPage,
            };
            ApiResponse<IEnumerable<EstudianteDto>> response = new ApiResponse<IEnumerable<EstudianteDto>>(estudianteDto)
            {
                Meta = metadata
            };
            return Ok(response);
        }
        /// <summary>
        /// Retrieve Student
        /// </summary>
        /// <param name="id">The ID of the student to retrieve</param>
        /// <returns></returns>
        [HttpGet("GetEstudiante/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<EstudianteDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetEstudiante(int id)
        {
            Estudiante? estudiante = await _estudianteService.GetEstudiante(id);
            EstudianteDto estudianteDto = _mapper.Map<EstudianteDto>(estudiante);
            ApiResponse<EstudianteDto> response = new ApiResponse<EstudianteDto>(estudianteDto);
            return Ok(response);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="estudiante">Student data</param>
        /// <returns></returns>
        [HttpPost("CreateEstudiante")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<EstudianteDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateEstudiante(EstudianteDto estudianteDto)
        {
            Estudiante estudiante = _mapper.Map<Estudiante>(estudianteDto);
            await _estudianteService.InsertEstudiante(estudiante);
            ApiResponse<EstudianteDto> response = new ApiResponse<EstudianteDto>(estudianteDto);
            return Ok(response);
        }
        /// <summary>
        /// Update a student
        /// </summary>   
        /// <param name="id">The ID of the student to update</param>
        /// <param name="EstudianteDto">Updated student data</param>
        /// <returns></returns>
        [HttpPut("UpdateEstudiante")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateEstudiante(int id, EstudianteDto estudianteDto)
        {
            Estudiante estudiante = _mapper.Map<Estudiante>(estudianteDto);
            estudiante.Id = id;
            bool result = await _estudianteService.UpdateEstudiante(estudiante);
            ApiResponse<bool> response = new ApiResponse<bool>(result);
            return Ok(response);
        }
        /// <summary>
        /// Delete a student by ID
        /// </summary>    
        /// <param name="id">The ID of the student to delete</param>
        /// <returns></returns>
        [HttpDelete("DeleteEstudiante/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            bool result = await _estudianteService.DeleteEstudiante(id);
            ApiResponse<bool> response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
