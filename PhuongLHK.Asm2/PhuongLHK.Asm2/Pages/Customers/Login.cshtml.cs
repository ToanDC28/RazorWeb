using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhuongLHK.Asm2.Repo.Repositories;
using System.ComponentModel.DataAnnotations;

namespace PhuongLHK.Asm2.Web.Pages.Customers
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
            public string Phone { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("CustomerID") != null)
            {
                return RedirectToPage("/Products/Shopping");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Products/Shopping");
            
            var result = unitOfWork._context.Customers.FirstOrDefault(a => a.Phone == Input.Phone && a.Password == Input.Password);
            if (result != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("CustomerID", result.CustomerId.ToString());
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid login attempt.");
                return Page();
            }
        }
    }
}
