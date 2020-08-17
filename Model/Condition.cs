using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClinicWeb.Model
{
    public partial class Condition
    {
        public Condition()
        {
            Diagnosis = new HashSet<Diagnosis>();
        }

        public int ConditionId { get; set; }
        [DisplayName("Condition Name")]
        public string ConditionName { get; set; }
        [DisplayName("Condition Description")]
        public string ConditionDescription { get; set; }

        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
    }
}
