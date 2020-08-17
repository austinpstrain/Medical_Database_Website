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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public MedicalTest MedicalTest { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        private MySqlConnection connection;
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient = repo.GetPatient(id);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var connStr = "Database=clinicdb; Data Source=team5med-db.mysql.database.azure.com; User Id=Team5DBAdmin@team5med-db; Password=Clinic123;Allow User Variables=True";
            connection = new MySqlConnection(connStr);
            connection.Open();
            var cmd = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = transaction;

            cmd.CommandText = @"INSERT INTO medical_test(patient_id, name, details, result)
                                values(@PatientID, @Name, @Details, @Result)";
            cmd.Parameters.Add("@PatientID", MySqlDbType.Int32).Value = Patient.PatientId;
            cmd.Parameters.Add("@Details", MySqlDbType.Text).Value = MedicalTest.Details;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/MedicalTests/View");
        }
    }
}