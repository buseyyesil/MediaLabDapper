using MediaLabDapper.Repositories.AppointmentRepositories;
using MediaLabDapper.Repositories.DepartmentRepositories;
using MediaLabDapper.Repositories.DoctorRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController(
        IAppointmentRepository _appointmentRepository,
        IDepartmentRepository _departmentRepository,
        IDoctorRepositories _doctorRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            var doctors = await _doctorRepository.GetAllDoctorAsync();
            var departments = await _departmentRepository.GetAllDepartmentsAsync();

            ViewBag.TotalAppointments = appointments.Count();
            ViewBag.TotalDoctors = doctors.Count();
            ViewBag.TotalDepartments = departments.Count();
            ViewBag.PendingAppointments = appointments.Count(x => (int)x.IsApproved == 0);
            ViewBag.ApprovedAppointments = appointments.Count(x => (int)x.IsApproved == 1);
            ViewBag.RejectedAppointments = appointments.Count(x => (int)x.IsApproved == 2);
            ViewBag.LastAppointments = appointments.OrderByDescending(x => x.Date).Take(5).ToList();
            var monthlyData = appointments
    .GroupBy(x => x.Date.Month)
    .OrderBy(x => x.Key)
    .Select(x => new { Month = x.Key, Count = x.Count() })
    .ToList();

            ViewBag.MonthlyLabels = string.Join(",", monthlyData.Select(x => $"'{new DateTime(2026, x.Month, 1).ToString("MMMM")}'"));
            ViewBag.MonthlyData = string.Join(",", monthlyData.Select(x => x.Count));
            return View();
        }
    }
}