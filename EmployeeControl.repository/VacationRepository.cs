using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using EmployeeContol.model;
using EmployeeContol.model.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;

namespace EmployeeControl.repository
{
    public class VacationRepository : IVacationRepository
    {
        private IConfiguration _configuration;

        public VacationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<VacationRequest>> GetAsync(int userId, int roleId)
        {
            try
            {
                using (var connection = DataLayer.GetConnection(_configuration))
                {
                    var sql = roleId != (int) RoleEnum.Employer
                        ? $"select * from VacationRequest where UserId = @userId"
                        : $"select * from VacationRequest";

                    var requestList = await connection.QueryAsync<VacationRequest>(sql, new {userId});

                    return requestList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<VacationRequest> ChangeStatusAsync(VacationRequestParameter vacationRequestParameter)
        {
            try
            {
                using (var connection = DataLayer.GetConnection(_configuration))
                {
                    var updateSql = "UPDATE VacationRequest SET Status = @status WHERE Id = @id;";


                    var updateExecuteResult = await connection.ExecuteAsync(updateSql,
                        new
                        {
                            status = vacationRequestParameter.VacationStatus,
                            id = vacationRequestParameter.VacationRequestId
                        });
                    Debug.WriteLine(updateExecuteResult);

                    var updatedVacationRequest =
                        await connection.GetAsync<VacationRequest>(vacationRequestParameter.VacationRequestId);
                    return updatedVacationRequest;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}