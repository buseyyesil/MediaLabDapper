using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.AboutDtos;
namespace MediaLabDapper.Repositories.AboutRepositories
{
    public class AboutRepository : IAboutRepository
    {
        private readonly DapperContext _context;
        public AboutRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            string query = "INSERT INTO Abouts (Description, Title1, Explanation1, Title2, Explanation2, Title3, Explanation3) VALUES (@Description, @Title1, @Explanation1, @Title2, @Explanation2, @Title3, @Explanation3)";
            var parameters = new DynamicParameters(createAboutDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteAboutAsync(int id)
        {
            string query = "DELETE FROM Abouts WHERE AboutId = @AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("AboutId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultAboutDto>> GetAllAboutsAsync()
        {
            string query = "SELECT * FROM Abouts";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultAboutDto>(query);
        }
        public async Task<GetByIdAboutDto> GetAboutByIdAsync(int id)
        {
            string query = "SELECT * FROM Abouts WHERE AboutId = @AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("AboutId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdAboutDto>(query, parameters);
        }
        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            string query = "UPDATE Abouts SET Description = @Description, Title1 = @Title1, Explanation1 = @Explanation1, Title2 = @Title2, Explanation2 = @Explanation2, Title3 = @Title3, Explanation3 = @Explanation3 WHERE AboutId = @AboutId";
            var parameters = new DynamicParameters(updateAboutDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}