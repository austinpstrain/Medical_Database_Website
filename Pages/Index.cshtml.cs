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
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
        
        public void OnPost()
        {
             var username = Request.Form["username"];
             var password = Request.Form["password"];
        }
    }
}
