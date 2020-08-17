using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using ClinicWeb.Util;
using ClinicWeb.Security;

namespace ClinicWeb.Pages
{
    public class MeModel : PageModel
    {
        public IActionResult OnGet()
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Patient)
                return Redirect("/Login");

            return Page();
        }
    }
}
