using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HaripayTechnicalAssessmentData.Model;
using HaripayTechnicalAssessmentData.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using HaripayTechnicalAssessmentData;

namespace HaripayTechnicalAssessmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HaripayDataContext _context;

        public DepartmentService(HaripayDataContext context)
        {
            _context = context;
        }

        public bool Create(string departmentName, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (String.IsNullOrWhiteSpace(departmentName))
                {
                    message = "Department Name is required.";
                    return result;
                }

                Department department = new Department()
                {
                    Name = departmentName,
                };
                _context.Departments.Add(department);
                _context.SaveChanges();
                result = true;
            }
            catch(Exception error)
            {
                message = error.Message;
                result = false;
            }

            return result;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public bool Update(long departmentId, string departmentName, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (String.IsNullOrWhiteSpace(departmentName))
                {
                    message = "Department Name is required";
                    return result;
                }

                if (departmentId <= 0)
                {
                    message = "Department Id is invalid";
                    return result;
                }

                Department department = _context.Departments.FirstOrDefault(x => x.Id == departmentId);
                if(department == null)
                {
                    message = "No department with supplied ID exists";
                    return result;
                }
                department.Name = departmentName;

                _context.Departments.Update(department);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception error)
            {
                message = error.Message;
                result = false;
            }

            return result;
        }
    }
}
