using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.DepartmentDtos;

namespace MediaLabDapper.Repositories.DepartmentRepositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
           string query = "INSERT INTO departments (departmentName,description) VALUES (@DepartmentName,@Description)";
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentName", createDepartmentDto.DepartmentName);
            parameters.Add("Description", createDepartmentDto.Description);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            string query = "DELETE FROM Departments WHERE DepartmentId = @DepartmentId";
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<ResultDepartmentDto>> GetAllDepartmentsAsync()
        {
            string query = "SELECT * FROM Departments";
            var connection = _context.CreateConnection();
    
    
            return await connection.QueryAsync<ResultDepartmentDto>(query);
        }

        public async Task<GetDepartmentByIdDto> GetDepartmentByIdAsync(int id)
        {
            string query = "SELECT * FROM Departments WHERE DepartmentId = @DepartmentId";
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetDepartmentByIdDto>(query, parameters);
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            string query = "UPDATE Departments SET DepartmentName = @DepartmentName, Description = @Description WHERE DepartmentId = @DepartmentId";
            var parameters = new DynamicParameters(updateDepartmentDto); 
          
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
