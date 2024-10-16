using University.Core.Entities;
using University.Core.Interfaces;
using University.Infraestructure.Data;

namespace University.Infraestructure.Repositories
{
    public class EstudianteRepository : BaseRepository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(UniversityStoreContext context) : base(context)
        {
        }
    }
}
