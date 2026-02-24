using MediaLabDapper.Repositories.AboutRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "AboutViewComponent")]
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutViewComponent(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _aboutRepository.GetAllAboutsAsync();
            return View(about.FirstOrDefault());
        }
    }
}