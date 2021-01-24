using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Dapper;
using EmployeeContol.model;
using EmployeeContol.model.Enum;
using Microsoft.Extensions.Configuration;

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
    }
}