using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.FeatureDto;
using MediaLabDapper.DTOs.FeatureDtos;
namespace MediaLabDapper.Repositories.FeatureRepositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly DapperContext _context;
        public FeatureRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            string query = "INSERT INTO Features (Title, Description, Icon) VALUES (@Title, @Description, @Icon)";
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new
            {
                createFeatureDto.Title,
                createFeatureDto.Description,
                createFeatureDto.Icon
            });
        }
        public async Task DeleteFeatureAsync(int id)
        {
            string query = "DELETE FROM Features WHERE FeatureId = @FeatureId";
            var parameters = new DynamicParameters();
            parameters.Add("FeatureId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultFeatureDto>> GetAllFeaturesAsync()
        {
            string query = "SELECT * FROM Features";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultFeatureDto>(query);
        }
        public async Task<GetByIdFeatureDto> GetFeatureByIdAsync(int id)
        {
            string query = "SELECT * FROM Features WHERE FeatureId = @FeatureId";
            var parameters = new DynamicParameters();
            parameters.Add("FeatureId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdFeatureDto>(query, parameters);
        }
        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            string query = "UPDATE Features SET Title = @Title, Description = @Description, Icon = @Icon WHERE FeatureId = @FeatureId";
            var parameters = new DynamicParameters(updateFeatureDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}