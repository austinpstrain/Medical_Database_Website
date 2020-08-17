using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
            Diagnosis = new HashSet<Diagnosis>();
            Prescription = new HashSet<Prescription>();
            Schedule = new HashSet<Schedule>();
            WorksAt = new HashSet<WorksAt>();
        }

        public int DoctorId { get; set; }
        public int PersonId { get; set; }
        public int? SpecializationId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Specializations Specialization { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<WorksAt> WorksAt { get; set; }
    }
}
