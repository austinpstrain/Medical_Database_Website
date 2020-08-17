using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Office
    {
        public Office()
        {
            Appointment = new HashSet<Appointment>();
            Order = new HashSet<Order>();
            Patient = new HashSet<Patient>();
            Schedule = new HashSet<Schedule>();
            WorksAt = new HashSet<WorksAt>();
        }

        public int OfficeId { get; set; }
        public int? AddressId { get; set; }
        public string Phone { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<WorksAt> WorksAt { get; set; }
    }
}
