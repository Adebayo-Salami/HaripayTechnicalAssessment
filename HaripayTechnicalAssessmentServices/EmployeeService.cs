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
    public class EmployeeService : IEmployeeService
    {
        private readonly HaripayDataContext _context;

        public EmployeeService(HaripayDataContext context)
        {
            _context = context;
        }

        public bool Create(string name, long departmentId, string profilePicName, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if(departmentId <= 0)
                {
                    message = "Invalid Department Id";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(name))
                {
                    message = "Name is required";
                    return result;
                }

                Employee employee = new Employee()
                {
                    Name = name,
                    Department = _context.Departments.FirstOrDefault(x => x.Id == departmentId),
                    ProfileFileName = profilePicName,
                    DateJoining = DateTime.Now
                };
                _context.Employees.Add(employee);
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

        public List<Employee> GetAll()
        {
            return _context.Employees.Include(x => x.Department).ToList();
        }

        public bool Update(long employeeId, string name, string profilePicName, out string message)
        {
            bool result = false;
            message = String.Empty;

            try
            {
                if (employeeId <= 0)
                {
                    message = "Invalid Employee Id";
                    return result;
                }

                if (String.IsNullOrWhiteSpace(name))
                {
                    message = "Name is required";
                    return result;
                }

                Employee employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
                employee.Name = (String.IsNullOrWhiteSpace(name)) ? employee.Name : name;
                employee.ProfileFileName = (String.IsNullOrWhiteSpace(profilePicName)) ? employee.ProfileFileName : profilePicName;

                _context.Employees.Update(employee);
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
