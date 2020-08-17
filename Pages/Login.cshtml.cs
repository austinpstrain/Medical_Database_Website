using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using ClinicWeb.Util;
using ClinicWeb.Security;

namespace ClinicWeb.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                new AuthService().Login(HttpContext, Username, Password);
                return Redirect("/");
            }
            catch
            {
                ModelState.AddModelError("Password", "Invalid username or password");
            }

            return Page();
        }
    }
}
