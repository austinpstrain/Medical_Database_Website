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

namespace ClinicWeb.Pages.Orders
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public Prescription Prescription { get; set; }
        [BindProperty]
        public IEnumerable<Office> Office { get; set; }
        private MySqlConnection connection;

        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Prescription = repo.GetPrescription(id);
                Office = repo.ReadOffices();
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            connection = new MySqlConnection(ConnectionStrings.Default);
            connection.Open();
            var cmd = connection.CreateCommand();
            /*MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = transaction;*/

            cmd.CommandText = @"INSERT INTO `order` (prescription_id, office_id) values(@PrescriptionID, @OfficeID)";
            cmd.Parameters.Add("@PrescriptionID", MySqlDbType.Int32).Value = Prescription.PrescriptionId;
            cmd.Parameters.Add("@OfficeID", MySqlDbType.Int32).Value = Order.OfficeId;
            cmd.ExecuteNonQuery();
            //transaction.Commit();
            connection.Close();
            return RedirectToPage("/patients/patientinfo");
        }
    }
}