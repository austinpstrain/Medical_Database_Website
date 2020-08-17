using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class WorksAt
    {
        public int DoctorOfficesId { get; set; }
        public int DoctorId { get; set; }
        public int OfficeId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Office Office { get; set; }
    }
}
