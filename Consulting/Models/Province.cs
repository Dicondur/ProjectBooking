using System;
using System.Collections.Generic;

namespace Consulting.Models
{
    public partial class Province
    {
        public Province()
        {
            Customer = new HashSet<Customer>();
        }

        public string ProvinceCode { get; set; }
        public string Name { get; set; }
        public double ProvincialTax { get; set; }

        public ICollection<Customer> Customer { get; set; }
    }
}
