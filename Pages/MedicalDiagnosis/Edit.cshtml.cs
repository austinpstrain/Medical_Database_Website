using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicWeb.Model;
using ClinicWeb.Services;
using ClinicWeb.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.MedicalDiagnosis
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Diagnosis Diagnosis { get; set; }
        [BindProperty]
        public IEnumerable<Doctor> Doctor { get; set; }
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Diagnosis = repo.GetDiagnosis(id);
                Doctor = repo.ReadDoctors();
            }
        }
        private MySqlConnection connection;
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

            cmd.CommandText = @"UPDATE condition_t SET condition_name = @ConditionName, condition_description = @ConditionDescription
                                WHERE condition_id = @ConditionID";
            cmd.Parameters.Add("@ConditionName", MySqlDbType.String).Value = Diagnosis.Condition.ConditionName;
            cmd.Parameters.Add("@ConditionDescription", MySqlDbType.String).Value = Diagnosis.Condition.ConditionDescription;
            cmd.Parameters.Add("@ConditionID", MySqlDbType.Int32).Value = Diagnosis.ConditionId;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"UPDATE diagnosis SET doctor_id = @DoctorID, details = @Details 
                                WHERE diagnosis_id = @DiagnosisID";
            cmd.Parameters.Add("@DoctorID", MySqlDbType.Int32).Value = Diagnosis.DoctorId;
            cmd.Parameters.Add("@Details", MySqlDbType.String).Value = Diagnosis.Details;
            cmd.Parameters.Add("@DiagnosisID", MySqlDbType.Int32).Value = Diagnosis.DiagnosisId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/MedicalDiagnosis/View");
            //return RedirectToPage("/patients/patientinfo");

        }
    }
}
