using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Model
{
    public class Discount
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal PercentageValue { get; set; }
        public DateTime DateCreated { get; set; }
        public DiscountStatus Status { get; set; }
    }
}
