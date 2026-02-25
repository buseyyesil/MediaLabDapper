using MediaLabDapper.DTOs.TestimonialDto;
using MediaLabDapper.Repositories.TestimonialRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TestimonialController(ITestimonialRepository _testimonialRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var testimonials = await _testimonialRepository.GetAllTestimonialsAsync();
            return View(testimonials);
        }
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _testimonialRepository.DeleteTestimonialAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateTestimonial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            await _testimonialRepository.CreateTestimonialAsync(createTestimonialDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var testimonial = await _testimonialRepository.GetTestimonialByIdAsync(id);
            var updateDto = new UpdateTestimonialDto
            {
                TestimonialId = testimonial.TestimonialId,
                FullName = testimonial.FullName,
                Title = testimonial.Title,
                Comment = testimonial.Comment,
                Review = testimonial.Review,
                ImageUrl = testimonial.ImageUrl
            };
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            await _testimonialRepository.UpdateTestimonialAsync(updateTestimonialDto);
            return RedirectToAction("Index");
        }
    }
}