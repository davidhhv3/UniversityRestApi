using FluentValidation;
using University.Core.DTOs;

namespace University.Infraestructure.Validators
{
    public class StudentValidator : AbstractValidator<EstudianteDto>
    {
        public StudentValidator()
        {
            RuleFor(estudiante => estudiante.Nombre)
                .Length(1, 10)
                .WithMessage("La longitud del nombre del estudiante debe estar entre 1 y 10 caracteres")
                .NotNull()
                .WithMessage("El nombre del estudiante no puede ser nulo");

            RuleFor(estudiante => estudiante.CiudadId)
               .NotNull()
               .WithMessage("Debe especificar una ciudad");
        }
    }
}
