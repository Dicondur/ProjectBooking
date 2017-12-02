using System;
using System.Collections.Generic;

namespace Consulting.Models
{
    public partial class Consultant
    {
        public Consultant()
        {
            WorkSession = new HashSet<WorkSession>();
        }

        public int ConsultantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateHired { get; set; }
        public double HourlyRate { get; set; }
        public string Gender { get; set; }
        public bool Partner { get; set; }
        public int Category { get; set; }

        public ICollection<WorkSession> WorkSession { get; set; }
    }
}
