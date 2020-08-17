using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Specializations
    {
        public Specializations()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int SpecializationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
