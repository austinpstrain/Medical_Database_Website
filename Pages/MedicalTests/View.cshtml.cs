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

namespace ClinicWeb.Pages.MedicalTests
{
    public class ViewModel : PageModel
    {
        public IEnumerable<MedicalTest> Tests { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        private MySqlConnection connection;
        public void OnGet(int id)
        {
            Tests = new List<MedicalTest>();

            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient = repo.GetPatient(id);
                Tests = repo.ReadMedicalTest(id);
            }
        }
    }
}