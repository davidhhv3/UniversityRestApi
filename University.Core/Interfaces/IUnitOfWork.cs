namespace University.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IEstudianteRepository EstudianteRepository { get; }

        Task SaveChangesAsync();
    }
}
