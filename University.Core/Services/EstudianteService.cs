using Microsoft.Extensions.Options;
using University.Core.CustomEntities;
using University.Core.Entities;
using University.Core.Helpers;
using University.Core.Interfaces;
using University.Core.QueryFilters;

namespace University.Core.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public EstudianteService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<bool> DeleteEstudiante(int id)
        {
            await EstudianteServiceHelpers.VerifyClientExistence(id, _unitOfWork);
            await _unitOfWork.EstudianteRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Estudiante?> GetEstudiante(int Id)
        {
            Estudiante? estudiante = await EstudianteServiceHelpers.VerifyClientExistence(Id, _unitOfWork);
            return estudiante;
        }

        public async Task<PagedList<Estudiante>> GetEstudiantes(QueryFilter filters)
        {
            filters = EstudianteServiceHelpers.SetValueFilter(filters, _paginationOptions);
            List<Estudiante> estudiantes = await EstudianteServiceHelpers.VerifyEstudiantesExistence(_unitOfWork);
            PagedList<Estudiante> pagedStudents = PagedList<Estudiante>.Create(estudiantes, filters.PageNumber, filters.PageSize);
            return pagedStudents;
        }

        public async Task<bool> InsertEstudiante(Estudiante estudiante)
        {
            await _unitOfWork.EstudianteRepository.Add(estudiante);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEstudiante(Estudiante estudiante)
        {
            Estudiante? existingEstudiante = await EstudianteServiceHelpers.VerifyClientExistence(estudiante.Id, _unitOfWork);
            if (existingEstudiante != null)
            {
                existingEstudiante.Nombre = estudiante.Nombre;
                existingEstudiante.CiudadId = estudiante.CiudadId;
                await _unitOfWork.EstudianteRepository.Update(existingEstudiante);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
