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

namespace ClinicWeb.Pages.Orders
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        private MySqlConnection connection;
        public void OnGet(int id)
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Order = repo.getOrder(id);

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

            var date = Order.DateArrived.ToString("yyyy-MM-dd");
            cmd.CommandText = @"UPDATE `order` SET `date_arrived` = @DateArrived WHERE(`order_id` = @OrderID)";
            cmd.Parameters.Add("@DateArrived", MySqlDbType.String).Value = date;
            cmd.Parameters.Add("@OrderID", MySqlDbType.Int32).Value = Order.OrderId;
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToPage("/Orders/Index");
            //return RedirectToPage("/patients/patientinfo");

        }
    }
}