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

namespace ClinicWeb.Pages.Prescriptions
{
    public class ViewModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Prescription> Prescription { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient = repo.GetPatient(id);
                Prescription = repo.ReadPrescriptions(id);
                
            }
        }
    }
}