using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class MedicalTest
    {
        public int MedicalTestId { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public bool? Result { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
