using MediaLabDapper.DTOs.TestimonialDto;

namespace MediaLabDapper.Repositories.TestimonialRepositories
{
    public interface ITestimonialRepository
    {
        Task<IEnumerable<ResultTestimonialDto>> GetAllTestimonialsAsync();
        Task<GetByIdTestimonialDto> GetTestimonialByIdAsync(int id);
        Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto);
        Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto);
        Task DeleteTestimonialAsync(int id);
    }
}