using MediaLabDapper.DTOs.AppointmentDtos;
using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaLabDapper.Controllers
{
    public class AppointmentController(
        IAppointmentRepository _appointmentRepository,
        IDepartmentRepository _departmentRepository,
        IDoctorRepositories _doctorRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            return View(appointments);
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            var updateDto = new UpdateAppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                FullName = appointment.FullName,
                Email = appointment.Email,
                Phone = appointment.Phone,
                Date = appointment.Date,
                Time = appointment.Time,
                Message = appointment.Message,
                IsApproved = appointment.IsApproved,
                DoctorId = appointment.DoctorId,
                DepartmentId = appointment.DepartmentId
            };
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
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto)
        {
            await _appointmentRepository.UpdateAppointmentAsync(updateAppointmentDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            await _appointmentRepository.CreateAppointmentAsync(createAppointmentDto);
            TempData["Success"] = "true";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ApproveAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            var updateDto = new UpdateAppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                FullName = appointment.FullName,
                Email = appointment.Email,
                Phone = appointment.Phone,
                Date = appointment.Date,
                Time = appointment.Time,
                Message = appointment.Message,
                IsApproved = AppointmentStatusDto.Onaylı,
                DoctorId = appointment.DoctorId,
                DepartmentId = appointment.DepartmentId
            };
            await _appointmentRepository.UpdateAppointmentAsync(updateDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            var updateDto = new UpdateAppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                FullName = appointment.FullName,
                Email = appointment.Email,
                Phone = appointment.Phone,
                Date = appointment.Date,
                Time = appointment.Time,
                Message = appointment.Message,
                IsApproved = AppointmentStatusDto.Reddedildi,
                DoctorId = appointment.DoctorId,
                DepartmentId = appointment.DepartmentId
            };
            await _appointmentRepository.UpdateAppointmentAsync(updateDto);
            return RedirectToAction("Index");
        }
    }
}