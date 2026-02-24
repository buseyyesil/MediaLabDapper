using MediaLabDapper.Repositories.TestimonialRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "TestimonialsViewComponent")]
    public class TestimonialsViewComponent : ViewComponent
    {
        private readonly ITestimonialRepository _testimonialRepository;
        public TestimonialsViewComponent(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = await _testimonialRepository.GetAllTestimonialsAsync();
            return View(testimonials);
        }
    }
}