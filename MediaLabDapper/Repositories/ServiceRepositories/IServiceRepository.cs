using MediaLabDapper.DTOs.ServiceDto;

namespace MediaLabDapper.Repositories.ServiceRepositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ResultServiceDto>> GetAllServicesAsync();
        Task<GetByIdServiceDto> GetServiceByIdAsync(int id);
        Task CreateServiceAsync(CreateServiceDto createServiceDto);
        Task UpdateServiceAsync(UpdateServiceDto updateServiceDto);
        Task DeleteServiceAsync(int id);
    }
}