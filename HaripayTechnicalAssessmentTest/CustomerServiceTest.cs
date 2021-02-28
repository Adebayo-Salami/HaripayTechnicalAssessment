using Xunit;
using Moq;
using HaripayTechnicalAssessmentServices;
using HaripayTechnicalAssessmentData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using HaripayTechnicalAssessmentData.Model;
using System;

namespace HaripayTechnicalAssessmentTest
{
    public class CustomerServiceTest
    {
        [Fact]
        public void Customer_Fullname_Is_Null()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var customerService = new CustomerService(context);

            Assert.Throws<ArgumentException>(() => customerService.CreateCustomer(String.Empty, 0));
        }
    }
}
