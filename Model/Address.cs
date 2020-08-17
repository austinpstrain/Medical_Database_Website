using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicWeb.Model
{
    public partial class Address
    {
        public int AddressId { get; set; }
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [DisplayName("Postal Code")]
        public int PostalCode { get; set; }
    }
}
