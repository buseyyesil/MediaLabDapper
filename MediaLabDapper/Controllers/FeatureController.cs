using MediaLabDapper.DTOs.FeatureDto;
using MediaLabDapper.DTOs.FeatureDtos;
using MediaLabDapper.Repositories.FeatureRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FeatureController(IFeatureRepository _featureRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var features = await _featureRepository.GetAllFeaturesAsync();
            return View(features);
        }
        public async Task<IActionResult> DeleteFeature(int id)
        {
            await _featureRepository.DeleteFeatureAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateFeature()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureRepository.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var feature = await _featureRepository.GetFeatureByIdAsync(id);
            var updateDto = new UpdateFeatureDto
            {
                FeatureId = feature.FeatureId,
                Title = feature.Title,
                Description = feature.Description,
                Icon = feature.Icon
            };
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _featureRepository.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index");
        }
    }
}