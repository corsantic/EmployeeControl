using System.Threading.Tasks;
using EmployeeContol.model;

namespace EmployeeContol.service
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string userName, string password);
    }
}