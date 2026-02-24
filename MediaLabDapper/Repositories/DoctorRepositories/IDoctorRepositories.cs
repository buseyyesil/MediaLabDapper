using MediaLabDapper.DTOs.DoctorDtos;

namespace MediaLabDapper.Repositories.DoctorRepositories
{
    public interface  IDoctorRepositories
    {
        Task<IEnumerable<ResultDoctorDto>> GetAllDoctorAsync(); //IEnumerable<ResultDoctorDto> birden fazla doktor dönecek demektir.
        Task<IEnumerable<ResultDoctorWithDepartmentDto>> GetAllDoctorWithDepartmentAsync();
        Task<GetDoctorByIdDto> GetDoctorByIdAsync(int id); //Task<> → Asenkron çalışıyor (database’e gidiyor) GetDoctorByIdDto → Tek bir doktor nesnesi döner
        Task CreateDoctorAsync(CreateDoctorDto createDoctorDto);
        Task UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto);
        Task DeleteDoctorAsync(int id);
    }
}
