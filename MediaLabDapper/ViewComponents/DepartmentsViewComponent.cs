using MediaLabDapper.Repositories.DepartmentRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "DepartmentsViewComponent")]
    public class DepartmentsViewComponent : ViewComponent
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsViewComponent(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return View(departments);
        }
    }
}