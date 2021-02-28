using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Interface
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string fullname, int customerType);
        List<Customer> GetAllCustomers();
        Customer GetCustomerByID(long id);
        Customer GetCustomerByName(string name);
    }
}
