using System.Threading.Tasks;
using EmployeeContol.model;
using EmployeeControl.repository;

namespace EmployeeContol.service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            return await _repository.AuthenticateAsync(userName, password);
        }

        public async Task<User> GetAsync(int userId)
        {
            return await _repository.GetAsync(userId);
        }
    }
}