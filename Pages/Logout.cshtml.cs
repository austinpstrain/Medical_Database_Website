using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ClinicWeb.Security;

namespace ClinicWeb.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet() {
            new AuthService().Logout(HttpContext);
            return Redirect("/");
        }
    }
}