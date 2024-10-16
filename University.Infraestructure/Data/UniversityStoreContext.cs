using Microsoft.EntityFrameworkCore;
using University.Core.Entities;

namespace University.Infraestructure.Data
{
    public partial class UniversityStoreContext : DbContext
    {
        public UniversityStoreContext() { }

        public UniversityStoreContext(DbContextOptions<UniversityStoreContext> options) : base(options)
        {
        }
        public virtual DbSet<Estudiante> Estudiante { get; set; }

    }
}
