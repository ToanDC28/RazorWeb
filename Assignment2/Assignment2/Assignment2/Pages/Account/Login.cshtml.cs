using Assignment2Entity.Models;
using AssignmentRepository.Repository.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Pages.Account
{
    public class LoginModel : PageModel
    {
        private AccountRepository _accountRepository;

        public LoginModel()
        {
            _accountRepository = new AccountRepository(new PRN_Assignment2Context());
        }
        

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            var result = _accountRepository.login(Input.Email, Input.Password);
            if (result != null)
            {
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
