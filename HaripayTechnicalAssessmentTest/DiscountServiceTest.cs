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
    public class DiscountServiceTest
    {
        [Fact]
        public void Type_Is_Null_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.Throws<ArgumentException>(() => discountService.CreateDiscount(String.Empty, 20, 1, 1));
        }

        [Fact]
        public void Discount_Type_Is_Invalid_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.Throws<InvalidCastException>(() => discountService.CreateDiscount("Type 1", 20, 10, 1));
        }

        [Fact]
        public void Invalid_Percentage_Configured_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.Throws<ArgumentException>(() => discountService.CreateDiscount("Type 1", 200, 1, 1));
        }

        [Fact]
        public void Invalid_Variable_Configured_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.Throws<ArgumentException>(() => discountService.CreateDiscount("Type 1", 20, 2, 1));
        }

        [Fact]
        public void Discount_Status_Is_Invalid_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.Throws<InvalidCastException>(() => discountService.CreateDiscount("Type 1", 20, 1, 10));
        }

        [Fact]
        public void Success_When_Creating()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.IsType<Discount>(discountService.CreateDiscount("Type 1", 20, 1, 1));
        }

        [Fact]
        public void Returns_List_Of_Discounts()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            Assert.IsType<List<Discount>>(discountService.GetAllDiscounts());
        }

        [Fact]
        public void Get_Discount_By_Type()
        {
            var options = new DbContextOptionsBuilder<HaripayDataContext>()
                .UseInMemoryDatabase(databaseName: "Haripay")
                .Options;

            var context = new HaripayDataContext(options);
            var discountService = new DiscountService(context);

            discountService.CreateDiscount("Type 1", 20, 1, 1);
            discountService.CreateDiscount("Type 2", 20, 1, 1);

            Assert.IsType<Discount>(discountService.GetDiscountByType("Type 1"));
        }

    }
}
