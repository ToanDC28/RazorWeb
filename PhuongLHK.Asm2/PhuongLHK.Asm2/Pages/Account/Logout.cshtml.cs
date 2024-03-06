using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhuongLHK.Asm2.Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("Account") != null)
            {
                HttpContext.Session.Remove("Account");
                
            }
            if (HttpContext.Session.GetString("CustomerID") != null)
            {
                HttpContext.Session.Remove("CustomerID");

            }
            return RedirectToPage("/Products/Shopping");
        }
    }
}
