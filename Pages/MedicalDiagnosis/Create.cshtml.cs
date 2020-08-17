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

namespace ClinicWeb.Pages.MedicalDiagnosis
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Diagnosis Diagnosis { get; set; }
        [BindProperty]
        public Condition Condition { get; set; }
        [BindProperty]
        public IEnumerable<Office> Office { get; set; }
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
            var connStr = "Database=clinicdb; Data Source=team5med-db.mysql.database.azure.com; User Id=Team5DBAdmin@team5med-db; Password=Clinic123;Allow User Variables=True";
            connection = new MySqlConnection(connStr);
            connection.Open();
            var cmd = connection.CreateCommand();
            MySqlTransaction transaction;
            transaction = connection.BeginTransaction();
            cmd.Connection = connection;
            cmd.Transaction = transaction;

            cmd.CommandText = @"INSERT INTO condition_t(condition_name, condition_description)
                                values(@ConditionName, @ConditionDescription)";
            cmd.Parameters.Add("@ConditionName", MySqlDbType.String).Value = Condition.ConditionName;
            cmd.Parameters.Add("@ConditionDescription", MySqlDbType.String).Value = Condition.ConditionDescription;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO diagnosis(doctor_id, patient_id, condition_id, details)
                                values(@DoctorID, @PatientID, last_insert_id(), @Details)";
            cmd.Parameters.Add("@DoctorID", MySqlDbType.Int32).Value = Diagnosis.DoctorId;
            cmd.Parameters.Add("@PatientID", MySqlDbType.Int32).Value = Patient.PatientId;
            cmd.Parameters.Add("@Details", MySqlDbType.Text).Value = Diagnosis.Details;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/MedicalDiagnosis/View");
            //return RedirectToPage("/patients/patientinfo");
        }
    }
}