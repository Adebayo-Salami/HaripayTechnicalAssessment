using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaripayTechnicalAssessment.ViewModel
{
    public class CreateEmployeeViewModel
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long DepartmentId { get; set; }
        public string ProfilePicName { get; set; }
    }
}
