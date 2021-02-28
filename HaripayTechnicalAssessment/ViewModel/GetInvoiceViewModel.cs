using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaripayTechnicalAssessment.ViewModel
{
    public class GetInvoiceViewModel
    {
        public long CustomerID { get; set; }
        public decimal BillAmount { get; set; }
        public bool IsGrocery { get; set; }
        public long? DiscountID { get; set; }
    }
}
