using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ConditionId { get; set; }
        public string Details { get; set; }
        public DateTime Created { get; set; }

        public virtual Condition Condition { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
