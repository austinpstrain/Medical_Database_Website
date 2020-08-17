using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ClinicWeb.Model
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int PrescriptionId { get; set; }
        public int OfficeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateArrived { get; set; }
        public DateTime? Created { get; set; }

        public virtual Office Office { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
