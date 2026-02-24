using MediaLabDapper.DTOs.FeatureDto;
using MediaLabDapper.DTOs.FeatureDtos;
namespace MediaLabDapper.Repositories.FeatureRepositories
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<ResultFeatureDto>> GetAllFeaturesAsync();
        Task<GetByIdFeatureDto> GetFeatureByIdAsync(int id);
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(int id);
    }
}