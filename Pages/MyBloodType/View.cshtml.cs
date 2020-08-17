using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using MySql.Data.MySqlClient;
using ClinicWeb.Services;
using ClinicWeb.Util;

namespace ClinicWeb.Pages.MyBloodType
{
    public class ViewModel : PageModel
    {
        public IEnumerable<BloodType> Type { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        private MySqlConnection connection;
        public void OnGet(int id)
        {
            Type = new List<BloodType>();

            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient = repo.GetPatient(id);
                Type = repo.ReadBloodType(id);
            }
        }
    }
}