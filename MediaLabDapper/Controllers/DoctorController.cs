using MediaLabDapper.DTOs.DoctorDtos;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaLabDapper.Controllers
{
    public class DoctorController(IDoctorRepositories _doctorRepositories,IDepartmentRepository _departmentRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepositories.GetAllDoctorWithDepartmentAsync();
            return View(doctors);
        }
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            await _doctorRepositories.DeleteDoctorAsync(id);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorRepositories.GetDoctorByIdAsync(id);
            return View(doctor);
        }
        public async Task<IActionResult> CreateDoctor()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.departments = (from x in departments select new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentId.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto createDoctorDto)
        {
            await _doctorRepositories.CreateDoctorAsync(createDoctorDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateDoctor(int id)
        {
            var doctor = await _doctorRepositories.GetDoctorByIdAsync(id);
            var updateDto = new UpdateDoctorDto
            {
                DoctorId = doctor.DoctorId,
                NameSurname = doctor.NameSurname,
                Description = doctor.Description,
                ImageUrl = doctor.ImageUrl,
                DepartmentId = doctor.DepartmentId
            };
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.departments = departments.Select(x => new SelectListItem
            {
                Text = x.DepartmentName,
                Value = x.DepartmentId.ToString()
            }).ToList();
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorDto updateDoctorDto)
        {
            await _doctorRepositories.UpdateDoctorAsync(updateDoctorDto);
            return RedirectToAction("Index");

        }
    }
}
