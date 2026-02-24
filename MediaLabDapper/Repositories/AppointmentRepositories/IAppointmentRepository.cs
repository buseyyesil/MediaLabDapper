using MediaLabDapper.DTOs.AppointmentDtos;
namespace MediaLabDapper.Repositories.AppointmentRepositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync();
        Task<GetByIdAppointmentDto> GetAppointmentByIdAsync(int id);
        Task CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto);
        Task DeleteAppointmentAsync(int id);
    }
}