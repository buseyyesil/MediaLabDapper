using MediaLabDapper.DTOs.ContactDto;

namespace MediaLabDapper.Repositories.ContactRepositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ResultContactDto>> GetAllContactsAsync();
        Task<GetByIdContactDto> GetContactByIdAsync(int id);
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task UpdateContactAsync(UpdateContactDto updateContactDto);
        Task DeleteContactAsync(int id);
    }
}