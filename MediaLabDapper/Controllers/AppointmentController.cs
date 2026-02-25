using MediaLabDapper.DTOs.AppointmentDtos;
using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using MediaLabDapper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppointmentController(
        IAppointmentRepository _appointmentRepository,
        IDepartmentRepository _departmentRepository,
        IDoctorRepositories _doctorRepository,
        IEmailService _emailService) : Controller
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

            try
            {
                await _emailService.SendEmailAsync(
                    appointment.Email,
                    "Randevunuz Onaylandı - MediLab",
                    $@"<div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto;'>
                        <div style='background:#0d6efd;padding:20px;text-align:center;'>
                            <h2 style='color:#fff;margin:0;'>🏥 MediLab Hastanesi</h2>
                        </div>
                        <div style='padding:30px;background:#f9f9f9;'>
                            <h3 style='color:#0d6efd;'>Randevunuz Onaylandı! ✅</h3>
                            <p>Sayın <strong>{appointment.FullName}</strong>,</p>
                            <p>Randevunuz başarıyla onaylanmıştır.</p>
                            <table style='width:100%;border-collapse:collapse;margin:20px 0;'>
                                <tr style='background:#e8f0fe;'>
                                    <td style='padding:10px;border:1px solid #ddd;'><strong>Tarih</strong></td>
                                    <td style='padding:10px;border:1px solid #ddd;'>{appointment.Date:dd/MM/yyyy}</td>
                                </tr>
                                <tr>
                                    <td style='padding:10px;border:1px solid #ddd;'><strong>Saat</strong></td>
                                    <td style='padding:10px;border:1px solid #ddd;'>{appointment.Time}</td>
                                </tr>
                            </table>
                            <p style='color:#666;'>Sağlıklı günler dileriz.</p>
                        </div>
                        <div style='background:#0d6efd;padding:10px;text-align:center;'>
                            <p style='color:#fff;margin:0;font-size:12px;'>MediLab Hastanesi © 2026</p>
                        </div>
                    </div>"
                );
                TempData["EmailSuccess"] = "Mail başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["EmailError"] = $"Mail gönderilemedi: {ex.Message}";
            }

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