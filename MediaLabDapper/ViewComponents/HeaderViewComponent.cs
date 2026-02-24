using Microsoft.AspNetCore.Mvc;
namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "HeaderViewComponent")]
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}