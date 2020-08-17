using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int OfficeId { get; set; }
        public int PatientId { get; set; }
        public string Reasons { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DoctorId { get; set; }
        public bool Approved { get; set; }
        public bool Canceled { get; set; }
        public string CancelReason { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Office Office { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
