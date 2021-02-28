using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaripayTechnicalAssessment.ViewModel
{
    public class CreateDiscountViewModel
    {
        public string Type { get; set; }
        public decimal Value { get; set; }
        public int DicountType { get; set; }
        public bool Status { get; set; }
    }
}
