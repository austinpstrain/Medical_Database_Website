using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicWeb.Model
{
    public partial class Person
    {
        public Person()
        {
            Account = new HashSet<Account>();
            Insurance = new HashSet<Insurance>();
        }

        public int PersonId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("DOB")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public int AddressId { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        public virtual Address Address { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Insurance> Insurance { get; set; }
    }
}
