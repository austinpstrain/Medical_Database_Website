using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Account Accounts { get; set; }
        [BindProperty]
        public Patient Patient { get; set; }

        public void OnGet()
        {

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

            cmd.CommandText = @"INSERT INTO address(street_address, state, city, postal_code) values(@StreetAddress,
                                @State, @City, @PostalCode)";
            cmd.Parameters.Add("@StreetAddress", MySqlDbType.String);
            cmd.Parameters["@StreetAddress"].Value = Patient.Person.Address.StreetAddress;
            cmd.Parameters.Add("@State", MySqlDbType.String).Value = Patient.Person.Address.State;
            cmd.Parameters.Add("@City", MySqlDbType.String).Value = Patient.Person.Address.City;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.Int32).Value = Patient.Person.Address.PostalCode;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO person(first_name, last_name, dob, gender, address_id, phone)
                                values(@FirstName, @LastName, @Dob,
                                @Gender, last_insert_id(), @Phone)";
            cmd.Parameters.Add("@FirstName", MySqlDbType.String).Value = Patient.Person.FirstName;
            cmd.Parameters.Add("@LastName", MySqlDbType.String).Value = Patient.Person.LastName;
            cmd.Parameters.Add("@Dob", MySqlDbType.Date).Value = Patient.Person.Dob;
            cmd.Parameters.Add("@Gender", MySqlDbType.Bit).Value = Patient.Person.Gender;
            cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = Patient.Person.Phone;
            cmd.ExecuteNonQuery();
            cmd.CommandText = ("select Last_insert_id()");
            var id = cmd.ExecuteScalar();
            cmd.CommandText = "INSERT INTO account(person_id, username, password,admin) values(@ID, @Username, @Password, 0)";
            cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@Username", MySqlDbType.String).Value = Accounts.Username;
            cmd.Parameters.Add("@Password", MySqlDbType.String).Value = Accounts.Password;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO patient(person_id, primary_office_id) values(@ID, @POfficeId)";
            //cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@POfficeId", MySqlDbType.Int32).Value = Patient.PrimaryOfficeId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("/Index");
        }
    }
}
