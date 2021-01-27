using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeContol.model;

namespace EmployeeControl.repository
{
    public interface IVacationRepository
    {
        Task<IEnumerable<VacationRequest>> GetAsync(int userId, int roleId);
        Task<VacationRequest> ChangeStatusAsync(VacationRequestParameter vacationRequestParameter);
    }
}