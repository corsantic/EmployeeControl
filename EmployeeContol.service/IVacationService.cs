using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeContol.model;

namespace EmployeeContol.service
{
    public interface IVacationService
    {
        Task<IEnumerable<VacationRequest>> GetAsync(int userId, int roleId);
        Task<VacationRequest> ChangeStatusAsync(VacationRequestParameter vacationRequestParameter);
    }
}