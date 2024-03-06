using Assignment2Entity.Models;
using AssignmentRepository.Repository.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private AccountRepository accountRepository;

        public RegisterModel()
        {
            accountRepository = new AccountRepository(new PRN_Assignment2Context());
        }
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string fullname { get; set; }

        public string error { get; set; }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            try
            {
                TlAccount account = new TlAccount();
                account.UserName = username;
                account.FullName = fullname;
                account.Password = Password;
                account.Type = "Member";
                accountRepository.CreateUserAsync(account);
                return LocalRedirect(returnUrl);
            }catch (Exception ex)
            {
                error = ex.Message;
                return Page();
            }
        }
    }
}
