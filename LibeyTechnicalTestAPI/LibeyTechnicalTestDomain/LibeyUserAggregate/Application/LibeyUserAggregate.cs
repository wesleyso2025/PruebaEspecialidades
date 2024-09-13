using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;

        public LibeyUserAggregate(ILibeyUserRepository repository)
        {
            _repository = repository;
        }

        public void Create(UserUpdateorCreateCommand command)
        {
            var user = new LibeyUser(command.DocumentNumber, command.DocumentTypeId, command.Name, command.FathersLastName, command.MothersLastName, command.Address, command.UbigeoCode, command.Phone, command.Email, command.Password);
            _repository.Create(user);
        }

        public void Update(UserUpdateorCreateCommand command)
        {
            var existingUser = _repository.FindByDocumentNumber(command.DocumentNumber);
            if (existingUser != null)
            {
                existingUser.Update(command.Name, command.FathersLastName, command.MothersLastName, command.Address, command.UbigeoCode, command.Phone, command.Email, command.Password);
                _repository.Update(existingUser);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public void Delete(string documentNumber)
        {
            _repository.Delete(documentNumber);
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            return _repository.FindResponse(documentNumber);
        }

        public IEnumerable<LibeyUserResponse> GetAll()
        {
            return _repository.GetAll();  
        }

        public IEnumerable<LibeyUserResponse> GetAllByTerm(string term)
        {
            var users = _repository.GetAll();
            if (!string.IsNullOrWhiteSpace(term))
            {
                users = users.Where(u =>
                    u.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    u.FathersLastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    u.MothersLastName.Contains(term, StringComparison.OrdinalIgnoreCase));
            }
            return users;
        }
    }

}