using MediaLabDapper.Repositories.ContactRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.ViewComponents
{
    [ViewComponent(Name = "ContactViewComponent")]
    public class ContactViewComponent : ViewComponent
    {
        private readonly IContactRepository _contactRepository;
        public ContactViewComponent(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contact = await _contactRepository.GetAllContactsAsync();
            return View(contact.FirstOrDefault());
        }
    }
}