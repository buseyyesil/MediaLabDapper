using MediaLabDapper.Context;
using MediaLabDapper.Repositories.AboutRepositories;
using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.ContactRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using MediaLabDapper.Repositories.FeatureRepositories;
using MediaLabDapper.Repositories.ServiceRepositories;
using MediaLabDapper.Repositories.TestimonialRepositories;
using MediaLabDapper.Services;

namespace MediaLabDapper.Extensions
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<DapperContext>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDoctorRepositories, DoctorRepositories>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}