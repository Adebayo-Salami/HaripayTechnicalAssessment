using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public CustomerType CustomerType { get; set; }
        public string DateCreated { get; set; }
    }
}
