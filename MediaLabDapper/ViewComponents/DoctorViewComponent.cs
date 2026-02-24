using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "DoctorViewComponent")]
    public class DoctorViewComponent : ViewComponent
    {
        private readonly IDoctorRepositories _doctorRepository;
        public DoctorViewComponent(IDoctorRepositories doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorWithDepartmentAsync();
            return View(doctors);
        }
    }
}