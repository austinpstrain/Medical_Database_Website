using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class BloodType
    {
        public BloodType()
        {
            Patient = new HashSet<Patient>();
        }

        public int BloodTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
