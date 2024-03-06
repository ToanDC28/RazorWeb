using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhuongLHK.Asm2.Repo.Repositories;
using System.ComponentModel.DataAnnotations;

namespace PhuongLHK.Asm2.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private UnitOfWork unitOfWork;

        public LoginModel(UnitOfWork unit)
        {
            unitOfWork = unit;
        }
        

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("AccountID") != null)
            {
                return RedirectToPage("/Index");
            }
            if (HttpContext.Session.GetString("CustomerID") != null)
            {
                HttpContext.Session.Remove("CustomerID");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            var result = unitOfWork._context.Accounts.FirstOrDefault(a => a.UserName == Input.Username && a.Password == Input.Password);
            if (result != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("AccountID", result.AccountId.ToString());
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid login attempt.");
                return Page();
            }
        }
    }
}
