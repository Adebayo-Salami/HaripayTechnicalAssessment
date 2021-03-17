using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Interface
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        bool Create(string departmentName, out string message);
        bool Update(long departmentId, string departmentName, out string message);
    }
}
