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

namespace ClinicWeb.Pages.Doctors
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Doctor Doctor { get; set; }

        public IActionResult OnGet(int id)
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Unauthorized();

            var connStr = "Database=clinicdb; Data Source=team5med-db.mysql.database.azure.com; User Id=Team5DBAdmin@team5med-db; Password=Clinic123";
            using (var repo = new Repo(connStr))
            {
                Doctor = repo.GetDoctor(id);
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
            cmd.Parameters["@StreetAddress"].Value = Doctor.Person.Address.StreetAddress;
            cmd.Parameters.Add("@State", MySqlDbType.String).Value = Doctor.Person.Address.State;
            cmd.Parameters.Add("@City", MySqlDbType.String).Value = Doctor.Person.Address.City;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.Int32).Value = Doctor.Person.Address.PostalCode;
            cmd.Parameters.Add("@AddressID", MySqlDbType.Int32).Value = Doctor.Person.AddressId;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"UPDATE person SET first_name = @FirstName, last_name = @LastName, dob = @Dob, 
                                phone = @Phone WHERE person_id = @PersonID";
            cmd.Parameters.Add("@FirstName", MySqlDbType.String).Value = Doctor.Person.FirstName;
            cmd.Parameters.Add("@LastName", MySqlDbType.String).Value = Doctor.Person.LastName;
            cmd.Parameters.Add("@Dob", MySqlDbType.Date).Value = Doctor.Person.Dob;
            cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = Doctor.Person.Phone;
            cmd.Parameters.Add("@PersonID", MySqlDbType.Int32).Value = Doctor.PersonId;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "UPDATE doctor SET specialization_id = @SpecID WHERE doctor_id = @DocID";
            cmd.Parameters.Add("@SpecID", MySqlDbType.Int32).Value = Doctor.SpecializationId;
            cmd.Parameters.Add("@DocID", MySqlDbType.Int32).Value = Doctor.DoctorId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("Index");

        }
    }
}