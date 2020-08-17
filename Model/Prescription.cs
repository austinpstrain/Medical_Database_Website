using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Prescription
    {
        public Prescription()
        {
            Order = new HashSet<Order>();
        }

        public int PrescriptionId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Product { get; set; }
        public float Dosage { get; set; }
        public int Refill { get; set; }
        public string Instructions { get; set; }
        public DateTime Created { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
