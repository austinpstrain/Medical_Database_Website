using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int DoctorId { get; set; }
        public int OfficeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Created { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Office Office { get; set; }
    }
}
