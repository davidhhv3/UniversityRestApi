using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using University.Core.Interfaces;
using University.Core.Services;
using University.Infraestructure.Data;
using University.Infraestructure.Repositories;

namespace University.Infraestructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UniversityStoreContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Connection"))
           );
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IEstudianteService, EstudianteService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
