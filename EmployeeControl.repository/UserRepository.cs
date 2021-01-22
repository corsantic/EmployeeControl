using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EmployeeContol.model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeControl.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;


        public UserRepository(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            using (var connection = DataLayer.GetConnection(_configuration))
            {
                var sql =
                    $@"select * from User WHERE u.Email = @userName AND u.Password = @password AND u.isEnabled;";

                var enforcedCheckSql =
                    $@"select * from User WHERE u.Email = @userName AND u.isEnabled = 1";
                var userDictionary = new Dictionary<int, User>();
                var user = new User();

                try
                {
                    user = (await connection.QueryAsync<User>(enforcedCheckSql,
                               new {userName, password})).FirstOrDefault() ??
                           (await connection.QueryAsync<User>(sql,
                               new {userName, password})).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(DateTime.Now + "-" + e);
                    throw;
                }

                if (user == null) return user;


                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;

                return user;
            }
        }
    }
}