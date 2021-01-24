using System.Threading.Tasks;
using EmployeeContol.model;

namespace EmployeeControl.repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string userName, string password);
        Task<User> GetAsync(int userId);
    }
}