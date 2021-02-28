using HaripayTechnicalAssessmentData.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Interface
{
    public interface IDiscountService
    {
        List<Discount> GetAllDiscounts();
        Discount GetDiscountByType(string type);
        Discount CreateDiscount(string type, decimal percentage, int discountType, int status);
        decimal GetTotalInvoiceAmount(long customerId, decimal billAmount, bool isGrocery, long discountId = 0);
    }
}
