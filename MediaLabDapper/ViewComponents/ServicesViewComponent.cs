using MediaLabDapper.Repositories.ServiceRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "ServicesViewComponent")]
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IServiceRepository _serviceRepository;
        public ServicesViewComponent(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            return View(services);
        }
    }
}