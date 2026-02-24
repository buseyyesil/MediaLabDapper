using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.ServiceDto;

namespace MediaLabDapper.Repositories.ServiceRepositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DapperContext _context;
        public ServiceRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            string query = "INSERT INTO Services (Title, Description, Icon) VALUES (@Title, @Description, @Icon)";
            var parameters = new DynamicParameters(createServiceDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteServiceAsync(int id)
        {
            string query = "DELETE FROM Services WHERE ServicesId = @ServicesId";
            var parameters = new DynamicParameters();
            parameters.Add("ServicesId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultServiceDto>> GetAllServicesAsync()
        {
            string query = "SELECT * FROM Services";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultServiceDto>(query);
        }
        public async Task<GetByIdServiceDto> GetServiceByIdAsync(int id)
        {
            string query = "SELECT * FROM Services WHERE ServicesId = @ServicesId";
            var parameters = new DynamicParameters();
            parameters.Add("ServicesId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdServiceDto>(query, parameters);
        }
        public async Task UpdateServiceAsync(UpdateServiceDto updateServiceDto)
        {
            string query = "UPDATE Services SET Title = @Title, Description = @Description, Icon = @Icon WHERE ServicesId = @ServicesId";
            var parameters = new DynamicParameters(updateServiceDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}