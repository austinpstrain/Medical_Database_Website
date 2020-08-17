using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using ClinicWeb.Services;
using ClinicWeb.Security;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.Locations
{
    public class EditModel : PageModel
    {
        public Office Office { get; set; }
        public IActionResult OnGet(int id)
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Unauthorized();

            var connStr = "Database=clinicdb; Data Source=team5med-db.mysql.database.azure.com; User Id=Team5DBAdmin@team5med-db; Password=Clinic123";
            using (var repo = new Repo(connStr))
            {
                Office = repo.GetOffice(id);
            }
            
            return Page();
        }
    }
}