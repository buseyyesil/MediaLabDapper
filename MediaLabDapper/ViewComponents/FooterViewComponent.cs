using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "FooterViewComponent")]
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
