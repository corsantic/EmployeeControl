using System;
using Dapper.Contrib.Extensions;

namespace EmployeeContol.model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int VacationDateCount { get; set; }
        public int RoleId { get; set; }

        [Write(false)] public string Token { get; set; }
    }
}