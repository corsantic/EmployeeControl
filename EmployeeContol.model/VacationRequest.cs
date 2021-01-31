using System;
using Dapper.Contrib.Extensions;
using EmployeeContol.model.Enum;

namespace EmployeeContol.model
{
    [Table("VacationRequest")]
    public class VacationRequest
    {
        [Key] public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public VacationStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Write(false)] public User User { get; set; }
    }
}