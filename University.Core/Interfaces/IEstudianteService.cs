using University.Core.CustomEntities;
using University.Core.Entities;
using University.Core.QueryFilters;

namespace University.Core.Interfaces
{
    public interface IEstudianteService
    {
        Task<PagedList<Estudiante>> GetEstudiantes(QueryFilter filters);

        Task<Estudiante?> GetEstudiante(int Id);

        Task<bool> InsertEstudiante(Estudiante estudiante);

        Task<bool> UpdateEstudiante(Estudiante estudiante);

        Task<bool> DeleteEstudiante(int id);
    }
}
