using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                        ? $"select v.*, u.* from VacationRequest v inner join User u on u.Id = v.UserId where UserId = @userId"
                        : $"select v.*, u.* from VacationRequest v inner join User u on u.Id = v.UserId";

                    var requestList =
                        await connection.QueryAsync<VacationRequest, User, VacationRequest>(sql, (v, u) =>
                            {
                                v.User = u;
                                return v;
                            },
                            new {userId});


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

                    var userVacationRequestSql =
                        "select v.*, u.* from VacationRequest v inner join User u on u.Id = v.UserId where v.Id = @vacationId";

                    var updatedVacationRequest =
                        (await connection.QueryAsync<VacationRequest, User, VacationRequest>(userVacationRequestSql,
                            (v, u) =>
                            {
                                v.User = u;
                                return v;
                            }, new {vacationId = vacationRequestParameter.VacationRequestId})).SingleOrDefault();
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