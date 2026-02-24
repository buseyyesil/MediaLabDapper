using MediaLabDapper.Repositories.FeatureRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "FeatureViewComponent")]
    public class FeatureViewComponent : ViewComponent
    {
        private readonly IFeatureRepository _featureRepository;
        public FeatureViewComponent(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _featureRepository.GetAllFeaturesAsync();
            return View(features);
        }
    }
}