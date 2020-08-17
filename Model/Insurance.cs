using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Insurance
    {
        public Insurance()
        {
            Patient = new HashSet<Patient>();
        }

        public int InsuranceId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? GroupNo { get; set; }
        public string PolicyNo { get; set; }
        public int? PolicyHolderId { get; set; }

        public virtual Person PolicyHolder { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
