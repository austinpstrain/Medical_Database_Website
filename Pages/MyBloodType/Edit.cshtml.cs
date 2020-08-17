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

namespace ClinicWeb.Pages.MyBloodType
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BloodType BloodType { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Patient  = repo.GetPatient(id);
                BloodType = repo.GetBloodType(id);
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

            cmd.CommandText = @"UPDATE patient SET blood_type_id = 
                                (SELECT blood_type_id FROM blood_type WHERE bt_name = @Name) WHERE patient_id = @PatientID;";
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = BloodType.Name;
            cmd.Parameters.Add("@PatientID", MySqlDbType.String).Value = Patient.PatientId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/MyBloodType/View");
        }
    }
}
