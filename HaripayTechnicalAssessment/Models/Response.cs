﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaripayTechnicalAssessment.Models
{
    public class Response
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseObject { get; set; }
    }
}
