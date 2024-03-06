using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                HttpContext.Session.Remove("userID");
                return RedirectToPage("/Account/Login");
            }
            return RedirectToPage("/Error");
        }
    }
}
