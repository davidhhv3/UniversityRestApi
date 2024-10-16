using University.Core.CustomEntities;
using University.Core.Entities;
using University.Core.Exceptions;
using University.Core.Interfaces;
using University.Core.QueryFilters;

namespace University.Core.Helpers
{
    public class EstudianteServiceHelpers
    {
        internal static QueryFilter SetValueFilter(QueryFilter filters, PaginationOptions _paginationOptions)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            return filters;
        }
        internal static async Task<Estudiante> VerifyClientExistence(int id, IUnitOfWork _unitOfWork, string message = "El estudiante no está registrado")
        {
            Estudiante? estudiante = await _unitOfWork.EstudianteRepository.GetById(id);
            ObjectVerifier.VerifyExistence(estudiante, message);
            return estudiante;
        }
        internal static async Task<List<Estudiante>> VerifyEstudiantesExistence(IUnitOfWork _unitOfWork)
        {
            List<Estudiante> estudiantes = (await _unitOfWork.EstudianteRepository.GetAll()).ToList();
            ObjectVerifier.VerifyExistence(estudiantes, "Aún no hay estudiantes registrados", estudiantes.Count());
            return estudiantes;
        }
    }
}
