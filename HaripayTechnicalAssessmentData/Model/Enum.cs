using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Model
{
    public enum CustomerType
    {
        Normal = 1,
        Employee,
        Affiliate
    }

    public enum DiscountStatus { 
        Active = 1,
        Inactive
    }
    public enum DiscountType
    {
        Percentage = 1,
        Fixed
    }
}
