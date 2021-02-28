using Xunit;
using Moq;
using HaripayTechnicalAssessmentServices;
using HaripayTechnicalAssessmentData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;

namespace HaripayTechnicalAssessmentTest
{
    public class CustomerServiceTest
    {
        [Fact]
        public void Customer_Fullname_Is_Null_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            Assert.Throws<ArgumentException>(() => customerService.CreateCustomer(String.Empty, 0));
        }

        [Fact]
        public void CustomerType_Is_Zero_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            Assert.Throws<ArgumentException>(() => customerService.CreateCustomer("Test", 0));
        }

        [Fact]
        public void Invalid_CustomerType_Is_Passed_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            Assert.Throws<InvalidCastException>(() => customerService.CreateCustomer("Test", 20));
        }

        [Fact]
        public void Success_When_Creating_Customer()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            Assert.IsType<Customer>(customerService.CreateCustomer("Test", 1));
        }

        [Fact]
        public void Returns_List_Of_Customers()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            customerService.CreateCustomer("Test 1", 1);
            customerService.CreateCustomer("Test 2", 2);

            Assert.IsType<List<Customer>>(customerService.GetAllCustomers());
        }

        [Fact]
        public void Returns_Customer_By_ID()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            customerService.CreateCustomer("Test 1", 1);
            customerService.CreateCustomer("Test 2", 2);

            Assert.IsType<Customer>(customerService.GetCustomerByID(1));
        }

        [Fact]
        public void Returns_Customer_By_Name()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            customerService.CreateCustomer("Test 1", 1);
            customerService.CreateCustomer("Test 2", 2);

            Assert.IsType<Customer>(customerService.GetCustomerByName("Test 2"));
        }
    }
}
