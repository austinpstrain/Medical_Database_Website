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

namespace ClinicWeb.Pages.MedicalTests
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public MedicalTest MedicalTest { get; set; }
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                MedicalTest = repo.GetMedicalTest(id);
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

            cmd.CommandText = @"UPDATE medical_test SET name = @Name, details = @Details, result = @Result
                                WHERE medical_test_id = @MedicalTestID";
            cmd.Parameters.Add("@Name", MySqlDbType.String).Value = MedicalTest.Name;
            cmd.Parameters.Add("@Details", MySqlDbType.Text).Value = MedicalTest.Details;
            cmd.Parameters.Add("@Result", MySqlDbType.Bit).Value = MedicalTest.Result;
            cmd.Parameters.Add("@MedicalTestID", MySqlDbType.Int32).Value = MedicalTest.MedicalTestId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/MedicalTests/View");

        }
    }
}
