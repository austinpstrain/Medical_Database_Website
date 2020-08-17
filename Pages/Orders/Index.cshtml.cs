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
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Order> Order { get; set; }
        public void OnGet()
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Order = repo.ReadOrders();
                
            }

        }
    }
}