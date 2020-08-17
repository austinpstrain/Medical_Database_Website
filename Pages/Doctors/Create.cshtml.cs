﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicWeb.Model;
using ClinicWeb.Security;
using MySql.Data.MySqlClient;

namespace ClinicWeb.Pages.Doctors
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            var authService = new AuthService();
            var account = authService.GetSessionAccount(HttpContext);
            if (account == null || account.GetAccessLevel() < AccessLevel.Admin)
                return Unauthorized();

            return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; }
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
            cmd.Parameters["@StreetAddress"].Value = Doctor.Person.Address.StreetAddress;
            cmd.Parameters.Add("@State", MySqlDbType.String).Value = Doctor.Person.Address.State;
            cmd.Parameters.Add("@City", MySqlDbType.String).Value = Doctor.Person.Address.City;
            cmd.Parameters.Add("@PostalCode", MySqlDbType.Int32).Value = Doctor.Person.Address.PostalCode;
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"INSERT INTO person(first_name, last_name, dob, gender, address_id, phone)
                                values(@FirstName, @LastName, @Dob,
                                @Gender, last_insert_id(), @Phone)";
            cmd.Parameters.Add("@FirstName", MySqlDbType.String).Value = Doctor.Person.FirstName;
            cmd.Parameters.Add("@LastName", MySqlDbType.String).Value = Doctor.Person.LastName;
            cmd.Parameters.Add("@Dob", MySqlDbType.Date).Value = Doctor.Person.Dob;
            cmd.Parameters.Add("@Gender", MySqlDbType.Bit).Value = Doctor.Person.Gender;
            cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = Doctor.Person.Phone;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO doctor(person_id, specialization_id) values(last_insert_id(), @SpecId)";
            cmd.Parameters.Add("@SpecId", MySqlDbType.Int32).Value = Doctor.SpecializationId;
            cmd.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            return RedirectToPage("Index");
        }
    }
}