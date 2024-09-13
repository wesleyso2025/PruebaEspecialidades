using AutoMapper;
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly IMapper _mapper;
        private readonly Context _context;


        public LibeyUserRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Add(libeyUser);
            _context.SaveChanges();
        }

        public void Update(LibeyUser libeyUser)
        {
            var existingUser = _context.LibeyUsers.SingleOrDefault(x => x.DocumentNumber == libeyUser.DocumentNumber);
            if (existingUser != null)
            {
                existingUser.Update(libeyUser.Name, libeyUser.FathersLastName, libeyUser.MothersLastName, libeyUser.Address, libeyUser.UbigeoCode, libeyUser.Phone, libeyUser.Email, libeyUser.Password);
                _context.SaveChanges();
            }
        }

        public void Delete(string documentNumber)
        {
            var user = _context.LibeyUsers.SingleOrDefault(x => x.DocumentNumber == documentNumber);
            if (user != null)
            {
                _context.LibeyUsers.Remove(user);
                _context.SaveChanges();
            }
        }

        public LibeyUser FindByDocumentNumber(string documentNumber)
        {
            return _context.LibeyUsers.SingleOrDefault(x => x.DocumentNumber == documentNumber);
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var q = from libeyUser in _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber))
                    select new LibeyUserResponse()
                    {
                        DocumentNumber = libeyUser.DocumentNumber,
                        Active = libeyUser.Active,
                        Address = libeyUser.Address,
                        DocumentTypeId = libeyUser.DocumentTypeId,
                        Email = libeyUser.Email,
                        FathersLastName = libeyUser.FathersLastName,
                        MothersLastName = libeyUser.MothersLastName,
                        Name = libeyUser.Name,
                        Password = libeyUser.Password,
                        Phone = libeyUser.Phone
                    };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }

        public IEnumerable<LibeyUserResponse> GetAll()
        {
            var users= _context.LibeyUsers.ToList();
            return _mapper.Map<IEnumerable<LibeyUserResponse>>(users);

        }

        public IEnumerable<LibeyUserResponse> GetAllByTerm(string term)
        {
            var query = _context.LibeyUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                query = query.Where(user =>
                    user.Name.ToLower().Contains(term) ||
                    user.FathersLastName.ToLower().Contains(term) ||
                    user.MothersLastName.ToLower().Contains(term));
            }

            var users = query.ToList();
            return _mapper.Map<IEnumerable<LibeyUserResponse>>(users);
        }
    }
}