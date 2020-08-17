using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ClinicWeb.Model;
using ClinicWeb.Util;
using ClinicWeb.Security;
using ClinicWeb.Services;
using ServiceStack.Text;

namespace ClinicWeb.Pages.Portal
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ReportsModel : PageModel
    {
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Patient> Patients { get; set; }

        public IActionResult OnGet()
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Redirect("/Login");

            return Page();
        }

        public IActionResult OnPost(string dataset, int count)
        {
            if (!ModelState.IsValid)
                return Page();

            if (dataset == "address")
            {
                using (var repo = new Repo(ConnectionStrings.Default))
                {
                    Addresses = repo.ReadAddresses().Take(count);

                    return Page();
                }
            }
            else if (dataset == "patient")
            {
                using (var repo = new Repo(ConnectionStrings.Default))
                {
                    Patients = repo.ReadPatients().Take(count);

                    return Page();
                }
            }

            return null;
        }
    }
}
