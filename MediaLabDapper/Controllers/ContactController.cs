using MediaLabDapper.DTOs.ContactDto;
using MediaLabDapper.Repositories.ContactRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaLabDapper.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController(IContactRepository _contactRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return View(contacts);
        }
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactRepository.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult CreateContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactRepository.CreateContactAsync(createContactDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateContact(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            var updateDto = new UpdateContactDto
            {
                ContactId = contact.ContactId,
                Location = contact.Location,
                Phone = contact.Phone,
                Email = contact.Email
            };
            return View(updateDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _contactRepository.UpdateContactAsync(updateContactDto);
            return RedirectToAction("Index");
        }
    }
}