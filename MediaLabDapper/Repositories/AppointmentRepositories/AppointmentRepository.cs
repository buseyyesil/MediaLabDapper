using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.AppointmentDtos;
namespace MediaLabDapper.Repositories.AppointmentRepositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DapperContext _context;
        public AppointmentRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            string query = "INSERT INTO Appointments (FullName, Email, Phone, Date, Time, DepartmentId, DoctorId, Message, IsApproved) VALUES (@FullName, @Email, @Phone, @Date, @Time, @DepartmentId, @DoctorId, @Message, @IsApproved)";
            var parameters = new DynamicParameters(createAppointmentDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteAppointmentAsync(int id)
        {
            string query = "DELETE FROM Appointments WHERE AppointmentId = @AppointmentId";
            var parameters = new DynamicParameters();
            parameters.Add("AppointmentId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultAppointmentDto>> GetAllAppointmentsAsync()
        {
            string query = @"SELECT a.AppointmentId, a.FullName, a.Email, a.Phone, a.Date, a.Time, a.Message, a.IsApproved,
                            d.NameSurname, dep.DepartmentName
                            FROM Appointments a
                            INNER JOIN Doctors d ON a.DoctorId = d.DoctorId
                            INNER JOIN Departments dep ON a.DepartmentId = dep.DepartmentId";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultAppointmentDto>(query);
        }
        public async Task<GetByIdAppointmentDto> GetAppointmentByIdAsync(int id)
        {
            string query = "SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId";
            var parameters = new DynamicParameters();
            parameters.Add("AppointmentId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdAppointmentDto>(query, parameters);
        }
        public async Task UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto)
        {
            string query = "UPDATE Appointments SET FullName = @FullName, Email = @Email, Phone = @Phone, Date = @Date, Time = @Time, DepartmentId = @DepartmentId, DoctorId = @DoctorId, Message = @Message, IsApproved = @IsApproved WHERE AppointmentId = @AppointmentId";
            var parameters = new DynamicParameters(updateAppointmentDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}