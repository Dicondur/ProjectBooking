using System;
using System.Collections.Generic;

namespace Consulting.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
        }

        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public bool TaxExempt { get; set; }
        public int Category { get; set; }

        public Province ProvinceCodeNavigation { get; set; }
        public ICollection<Contract> Contract { get; set; }
    }
}
