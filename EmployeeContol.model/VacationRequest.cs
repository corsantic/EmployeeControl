using System;
using EmployeeContol.model.Enum;

namespace EmployeeContol.model
{
    public class VacationRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public VacationStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}