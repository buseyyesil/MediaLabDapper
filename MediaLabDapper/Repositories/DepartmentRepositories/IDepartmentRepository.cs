using MediaLabDapper.DTOs.DepartmentDtos;

namespace MediaLabDapper.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<ResultDepartmentDto>> GetAllDepartmentsAsync();
        Task<GetDepartmentByIdDto> GetDepartmentByIdAsync(int id);
        Task CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);
        Task DeleteDepartmentAsync(int id);
    }
}
