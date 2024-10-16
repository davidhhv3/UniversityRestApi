using University.Core.Interfaces;
using University.Infraestructure.Data;

namespace University.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityStoreContext _context;
        private readonly IEstudianteRepository? _estudianteRepository;


        public UnitOfWork(UniversityStoreContext context)
        {
            _context = context;
        }
        public IEstudianteRepository EstudianteRepository => _estudianteRepository ?? new EstudianteRepository(_context);

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
