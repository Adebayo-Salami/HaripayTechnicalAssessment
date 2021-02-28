using Microsoft.EntityFrameworkCore;
using HaripayTechnicalAssessmentData.Model;

namespace HaripayTechnicalAssessmentData
{
    public class HaripayDataContext : DbContext
    {
        public HaripayDataContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
