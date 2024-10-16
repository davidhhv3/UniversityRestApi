namespace University.Core.Entities
{
    public class Estudiante : BaseEntity
    {
        public string? Nombre { get; set; }
        public int CiudadId { get; set; }
    }
}
