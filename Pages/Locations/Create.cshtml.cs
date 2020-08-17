using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using ClinicWeb.Security;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.Offices
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Office Office { get; set; }
        private MySqlConnection connection;
        public IActionResult OnGet()
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Unauthorized();

            return Page();
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

            cmd.CommandText = @"INSERT INTO address(street_address, state, city, postal_code) values(@StreetAddress,
                                @State, @City, @PostalCode)";
            cmd.Parameters.Add("@StreetAddress", MySqlDbType.String);
            cmd.Parameters["@StreetAddress"].Value = Office.Address.StreetAddress;
            cmd.Parameters.Add("@State", MySqlDbType.String).Value = Office.Address.State;
            cmd.Parameters.Add("@City", MySqlDbType.String).Value = Office.Address.City;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.Int32).Value = Office.Address.PostalCode;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO office(address_id, phone)
                                values(last_insert_id(), @Phone)";
            cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = Office.Phone;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("./Index");
        }
    }
}