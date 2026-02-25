using MediaLabDapper.DTOs.AppointmentDtos;
using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "AppointmentViewComponent")]
    public class AppointmentViewComponent : ViewComponent
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDoctorRepositories _doctorRepository;

        public AppointmentViewComponent(IAppointmentRepository appointmentRepository, IDepartmentRepository departmentRepository, IDoctorRepositories doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _departmentRepository = departmentRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            var doctors = await _doctorRepository.GetAllDoctorAsync();

            ViewBag.Departments = departments.Select(x => new SelectListItem
            {
                Text = x.DepartmentName,
                Value = x.DepartmentId.ToString()
            }).ToList();

            ViewBag.Doctors = doctors.Select(x => new SelectListItem
            {
                Text = x.IsAvailable ? x.NameSurname : $"{x.NameSurname} (Müsait Değil)",
                Value = x.DoctorId.ToString(),
                Disabled = !x.IsAvailable
            }).ToList();

            return View(new CreateAppointmentDto());
        }
    }
}