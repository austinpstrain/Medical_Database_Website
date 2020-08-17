using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ClinicWeb.Model;
using ClinicWeb.Security;
using ClinicWeb.Util;
using ClinicWeb.Services;

namespace ClinicWeb.Pages.Me
{
    public class AppointmentsModel : PageModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }

        public IActionResult OnGet()
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Patient)
                return Redirect("/Login");

            Appointments = new AppointmentService().FindAppointmentsWithPerson(account.PersonId, 100);

            return Page();
        }
    }
}
