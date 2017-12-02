using System;
using System.Collections.Generic;

namespace Consulting.Models
{
    public partial class WorkSession
    {
        public int WorkSessionId { get; set; }
        public int ContractId { get; set; }
        public DateTime DateWorked { get; set; }
        public int ConsultantId { get; set; }
        public double HoursWorked { get; set; }
        public string WorkDescription { get; set; }
        public double HourlyRate { get; set; }
        public double ProvincialTax { get; set; }
        public double TotalChargeBeforeTax { get; set; }

        public Consultant Consultant { get; set; }
        public Contract Contract { get; set; }
    }
}
