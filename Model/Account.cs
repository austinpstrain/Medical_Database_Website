using System;
using System.Collections.Generic;

namespace ClinicWeb.Model
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int PersonId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Admin { get; set; }

        public virtual Person Person { get; set; }
    }
}
