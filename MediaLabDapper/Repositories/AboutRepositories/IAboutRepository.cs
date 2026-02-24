using MediaLabDapper.DTOs.AboutDtos;
namespace MediaLabDapper.Repositories.AboutRepositories
{
    public interface IAboutRepository
    {
        Task<IEnumerable<ResultAboutDto>> GetAllAboutsAsync();
        Task<GetByIdAboutDto> GetAboutByIdAsync(int id);
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task DeleteAboutAsync(int id);
    }
}