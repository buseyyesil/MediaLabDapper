using Dapper;
using MediaLabDapper.Context;
using MediaLabDapper.DTOs.ContactDto;

namespace MediaLabDapper.Repositories.ContactRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DapperContext _context;
        public ContactRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            string query = "INSERT INTO Contacts (Location, Phone, Email) VALUES (@Location, @Phone, @Email)";
            var parameters = new DynamicParameters(createContactDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task DeleteContactAsync(int id)
        {
            string query = "DELETE FROM Contacts WHERE ContactId = @ContactId";
            var parameters = new DynamicParameters();
            parameters.Add("ContactId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
        public async Task<IEnumerable<ResultContactDto>> GetAllContactsAsync()
        {
            string query = "SELECT * FROM Contacts";
            var connection = _context.CreateConnection();
            return await connection.QueryAsync<ResultContactDto>(query);
        }
        public async Task<GetByIdContactDto> GetContactByIdAsync(int id)
        {
            string query = "SELECT * FROM Contacts WHERE ContactId = @ContactId";
            var parameters = new DynamicParameters();
            parameters.Add("ContactId", id);
            var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<GetByIdContactDto>(query, parameters);
        }
        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            string query = "UPDATE Contacts SET Location = @Location, Phone = @Phone, Email = @Email WHERE ContactId = @ContactId";
            var parameters = new DynamicParameters(updateContactDto);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }
    }
}