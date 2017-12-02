using System;
using System.Collections.Generic;

namespace Consulting.Models
{
    public partial class Contract
    {
        public Contract()
        {
            WorkSession = new HashSet<WorkSession>();
        }

        public int ContractId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public double DaysDuration { get; set; }
        public double QuotedPrice { get; set; }
        public bool Closed { get; set; }
        public double TotalChargedToDate { get; set; }

        public Customer Customer { get; set; }
        public ICollection<WorkSession> WorkSession { get; set; }
    }
}
