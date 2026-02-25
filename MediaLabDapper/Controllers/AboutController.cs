using MediaLabDapper.DTOs.AboutDtos;
using MediaLabDapper.Repositories.AboutRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutController(IAboutRepository _aboutRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutRepository.GetAllAboutsAsync();
            return View(abouts);
        }
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutRepository.DeleteAboutAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutRepository.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var about = await _aboutRepository.GetAboutByIdAsync(id);
            return View(about);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutRepository.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index");
        }
    }
}