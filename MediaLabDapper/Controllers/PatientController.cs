using MediaLabDapper.DTOs.AppointmentDtos;
using MediaLabDapper.Models;
using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController(
        UserManager<AppUser> _userManager,
        IAppointmentRepository _appointmentRepository,
        IDepartmentRepository _departmentRepository,
        IDoctorRepositories _doctorRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.FullName = user.FullName;
            ViewBag.Email = user.Email;
            ViewBag.Phone = user.PhoneNumber;
            return View();
        }

        public async Task<IActionResult> MyAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            var myAppointments = appointments.Where(x => x.Email == user.Email).ToList();
            return View(myAppointments);
        }

        public async Task<IActionResult> NewAppointment()
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
                Text = x.NameSurname,
                Value = x.DoctorId.ToString()
            }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            dto.FullName = user.FullName;
            dto.Email = user.Email;
            dto.Phone = user.PhoneNumber;
            dto.IsApproved = AppointmentStatusDto.Beklemede;

            await _appointmentRepository.CreateAppointmentAsync(dto);
            TempData["Success"] = "true";
            return RedirectToAction("MyAppointments");
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }

            TempData["ProfileSuccess"] = "Profiliniz başarıyla güncellendi.";
            return RedirectToAction("Profile");
        }
    }
}