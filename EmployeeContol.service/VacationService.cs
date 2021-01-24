using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeContol.model;
using EmployeeControl.repository;

namespace EmployeeContol.service
{
    public class VacationService : IVacationService
    {
        private IVacationRepository _vacationRepository;

        public VacationService(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }


        public async Task<IEnumerable<VacationRequest>> GetAsync(int userId, int roleId)
        {

            return await _vacationRepository.GetAsync(userId, roleId);
        }

    }
}