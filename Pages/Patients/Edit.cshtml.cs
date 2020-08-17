using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using ClinicWeb.Services;
using ClinicWeb.Security;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.Patients
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Patient Patient { get; set; }

        public IActionResult OnGet(int id)
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Unauthorized();

            var connStr = "Database=clinicdb; Data Source=team5med-db.mysql.database.azure.com; User Id=Team5DBAdmin@team5med-db; Password=Clinic123";
            using (var repo = new Repo(connStr))
            {
                Patient = repo.GetPatient(id);
            }

            return Page();
        }

        private MySqlConnection connection;

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

            cmd.CommandText = @"UPDATE address SET street_address = @StreetAddress, state = @State, city = @City,
                                postal_code = @PostalCode WHERE address_id = @AddressID";
            cmd.Parameters.Add("@StreetAddress", MySqlDbType.String);
            cmd.Parameters["@StreetAddress"].Value = Patient.Person.Address.StreetAddress;
            cmd.Parameters.Add("@State", MySqlDbType.String).Value = Patient.Person.Address.State;
            cmd.Parameters.Add("@City", MySqlDbType.String).Value = Patient.Person.Address.City;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.Int32).Value = Patient.Person.Address.PostalCode;
            cmd.Parameters.Add("@AddressID", MySqlDbType.Int32).Value = Patient.Person.AddressId;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"UPDATE person SET first_name = @FirstName, last_name = @LastName, dob = @Dob, 
                                phone = @Phone WHERE person_id = @PersonID";

            cmd.Parameters.Add("@FirstName", MySqlDbType.String).Value = Patient.Person.FirstName;
            cmd.Parameters.Add("@LastName", MySqlDbType.String).Value = Patient.Person.LastName;
            cmd.Parameters.Add("@Dob", MySqlDbType.Date).Value = Patient.Person.Dob;
            //cmd.Parameters.Add("@Gender", MySqlDbType.Bit).Value = Patient.Person.Gender;
            cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = Patient.Person.Phone;
            cmd.Parameters.Add("@PersonID", MySqlDbType.Int32).Value = Patient.PersonId;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "UPDATE patient SET primary_office_id = @POfficeId WHERE patient_id = @PatientID";
            cmd.Parameters.Add("@POfficeId", MySqlDbType.Int32).Value = Patient.PrimaryOfficeId;
            cmd.Parameters.Add("@PatientID", MySqlDbType.Int32).Value = Patient.PatientId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("PatientInfo");
        }
    }
}