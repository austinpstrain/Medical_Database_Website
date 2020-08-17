using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ClinicWeb.Model;
using ClinicWeb.Services;
using ClinicWeb.Util;

namespace ClinicWeb.Pages
{
    public class OfficesModel : PageModel
    {
        public IEnumerable<Office> Offices { get; set; }

        public void OnGet()
        {
            using (var repo = new Repo(ConnectionStrings.Default))
            {
                Offices = repo.ReadOffices();
            }
        }
    }
}