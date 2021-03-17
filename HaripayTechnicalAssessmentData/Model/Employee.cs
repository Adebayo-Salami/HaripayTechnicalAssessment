using System;
using System.Collections.Generic;
using System.Text;

namespace HaripayTechnicalAssessmentData.Model
{
   public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public DateTime DateJoining { get; set; }
        public string ProfileFileName { get; set; }
    }
}
