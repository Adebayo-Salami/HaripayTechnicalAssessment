using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Interface
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        bool Create(string name, long departmentId, string profilePicName, out string message);
        bool Update(long employeeId, string name, string profilePicName, out string message);
    }
}
