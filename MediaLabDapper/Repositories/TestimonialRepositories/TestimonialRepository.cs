using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.TestimonialDto;

namespace MediaLabDapper.Repositories.TestimonialRepositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly DapperContext _context;
        public TestimonialRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto)
        {
            string query = "INSERT INTO Testimonials (FullName, Comment, Review, Title, ImageUrl) VALUES (@FullName, @Comment, @Review, @Title, @ImageUrl)";
            var parameters = new DynamicParameters(createTestimonialDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteTestimonialAsync(int id)
        {
            string query = "DELETE FROM Testimonials WHERE TestimonialId = @TestimonialId";
            var parameters = new DynamicParameters();
            parameters.Add("TestimonialId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultTestimonialDto>> GetAllTestimonialsAsync()
        {
            string query = "SELECT * FROM Testimonials";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultTestimonialDto>(query);
        }
        public async Task<GetByIdTestimonialDto> GetTestimonialByIdAsync(int id)
        {
            string query = "SELECT * FROM Testimonials WHERE TestimonialId = @TestimonialId";
            var parameters = new DynamicParameters();
            parameters.Add("TestimonialId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdTestimonialDto>(query, parameters);
        }
        public async Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            string query = "UPDATE Testimonials SET FullName = @FullName, Comment = @Comment, Review = @Review, Title = @Title, ImageUrl = @ImageUrl WHERE TestimonialId = @TestimonialId";
            var parameters = new DynamicParameters(updateTestimonialDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}