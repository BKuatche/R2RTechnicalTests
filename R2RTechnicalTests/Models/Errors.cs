using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace R2RTechnicalTests.Models
{
    public class Errors
    {
        public string OrderNumber { get; set; }
        public List<string> ErrorMessages { get; set; }

      
    }
}
