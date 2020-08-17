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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Prescription Prescription { get; set; }
        [BindProperty]
        public IEnumerable<Doctor> Doctor { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        private MySqlConnection connection;
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient = repo.GetPatient(id);
                Doctor = repo.ReadDoctors();
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
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = transaction;

            cmd.CommandText = @"INSERT INTO prescription(doctor_id, patient_id, product, dosage, refill, instructions)
                                values(@DoctorID, @PatientID, @Product, @Dosage, @Refill, @Instructions)";
            cmd.Parameters.Add("@DoctorID", MySqlDbType.Int32).Value = Prescription.DoctorId;
            cmd.Parameters.Add("@PatientID", MySqlDbType.Int32).Value = Patient.PatientId;
            cmd.Parameters.Add("@Product", MySqlDbType.String).Value = Prescription.Product;
            cmd.Parameters.Add("@Dosage", MySqlDbType.Float).Value = Prescription.Dosage;
            cmd.Parameters.Add("@Refill", MySqlDbType.Int32).Value = Prescription.Refill;
            cmd.Parameters.Add("@Instructions", MySqlDbType.Text).Value = Prescription.Instructions;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/Patients/PatientInfo");
        }
    }
}