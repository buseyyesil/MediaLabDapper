using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.DoctorDtos;
using System.Data;

namespace MediaLabDapper.Repositories.DoctorRepositories
{
    public class DoctorRepositories(DapperContext _context) : IDoctorRepositories
    {
        private readonly IDbConnection _db=_context.CreateConnection();


        public async Task CreateDoctorAsync(CreateDoctorDto createDoctorDto)
        {
            var query = "INSERT INTO Doctors (NameSurname, ImageUrl, Description, DepartmentId) VALUES (@NameSurname, @ImageUrl, @Description, @DepartmentId)";
            var parameters = new DynamicParameters(createDoctorDto);
            await _db.ExecuteAsync(query, parameters);

        }

        public async Task DeleteDoctorAsync(int id)
        {
           var query = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";
           var parameter = new DynamicParameters();
           parameter.Add("@DoctorId", id);
           await _db.ExecuteAsync(query, parameter);
        }

        public async Task<IEnumerable<ResultDoctorDto>> GetAllDoctorAsync()
        {
           var query = "SELECT*FROM Doctors";
            return await _db.QueryAsync<ResultDoctorDto>(query);
        }

        public async Task<IEnumerable<ResultDoctorWithDepartmentDto>> GetAllDoctorWithDepartmentAsync()
        {
            var query = @"SELECT d.DoctorId, d.NameSurname, d.ImageUrl, d.Description, dep.DepartmentName 
                        FROM Doctors d
                        INNER JOIN Departments dep ON d.DepartmentId = dep.DepartmentId"; //Amaç: Doktorları getirirken aynı zamanda hangi bölüme ait olduklarını da getirmek.
                                                                                          //Yani:Doktorun DepartmentId’siBölümün DepartmentId’si aynıysa o kayıtları birleştir

            return await _db.QueryAsync<ResultDoctorWithDepartmentDto>(query);
        }

        public async  Task<GetDoctorByIdDto> GetDoctorByIdAsync(int id)
        {
            var query = "SELECT*FROM Doctors WHERE DoctorId = @DoctorId";
            var parameter = new DynamicParameters();
            parameter.Add("@DoctorId", id);
            return await _db.QuerySingleOrDefaultAsync<GetDoctorByIdDto>(query, parameter);

        }

        public Task UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
        {
            var query = "UPDATE Doctors SET NameSurname = @NameSurname, ImageUrl = @ImageUrl, Description = @Description, DepartmentId = @DepartmentId WHERE DoctorId = @DoctorId";
            var parameters = new DynamicParameters(updateDoctorDto);
            return _db.ExecuteAsync(query, parameters);
        }
    }
}
