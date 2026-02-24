using MediaLabDapper.DTOs.ServiceDto;
using MediaLabDapper.Repositories.ServiceRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    public class ServiceController(IServiceRepository _serviceRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            return View(services);
        }
        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceRepository.DeleteServiceAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            await _serviceRepository.CreateServiceAsync(createServiceDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateService(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            var updateDto = new UpdateServiceDto
            {
                ServicesId = service.ServicesId,
                Title = service.Title,
                Description = service.Description,
                Icon = service.Icon
            };
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto updateServiceDto)
        {
            await _serviceRepository.UpdateServiceAsync(updateServiceDto);
            return RedirectToAction("Index");
        }
    }
}