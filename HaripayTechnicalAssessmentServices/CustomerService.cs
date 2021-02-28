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
    public class CustomerService : ICustomerService
    {
        private readonly HaripayDataContext _context;

        public CustomerService(HaripayDataContext context)
        {
            _context = context;
        }

        public Customer CreateCustomer(string fullname, int customerType)
        {
            Customer customer = null;

            try
            {
                if (String.IsNullOrWhiteSpace(fullname))
                {
                    throw new Exception("Full Name Is Required");
                }

                if (customerType <= 0)
                {
                    throw new Exception("Customer type is required when creating a customer");
                }

                CustomerType customerTypeCast = (CustomerType)customerType;

                customer = new Customer()
                {
                    Fullname = fullname,
                    CustomerType = customerTypeCast,
                    DateCreated = DateTime.Now,
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch(Exception err)
            {
                throw err;
            }

            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerByID(long id)
        {
            if(id <= 0)
            {
                throw new Exception("Invalid ID Passed");
            }

            return _context.Customers.FirstOrDefault(x => x.Id == id);
        }

        public Customer GetCustomerByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Invalid Name Passed");
            }

            return _context.Customers.FirstOrDefault(x => x.Fullname == name);
        }
    }
}
