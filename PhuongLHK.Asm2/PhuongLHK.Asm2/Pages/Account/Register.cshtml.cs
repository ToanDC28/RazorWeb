using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;
using System.Dynamic;

namespace PhuongLHK.Asm2.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UnitOfWork unitOfWork;

        public RegisterModel(UnitOfWork unit)
        {
            unitOfWork = unit;
        }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Fullname { get; set; }
        public string error { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AccountID") != null)
            {
                
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("/Index");

            try
            {
                TlAccount cus = new TlAccount();
                if(unitOfWork._context.Accounts.Any(a => a.UserName == Username))
                {
                    error = Username + " has existed";
                    return Page();
                }
                var AccountID = unitOfWork._context.Accounts.Max(c => c.AccountId);
                cus.AccountId = AccountID + 1;
                cus.UserName = Username;
                cus.FullName = Fullname;
                cus.Password = Password;
                cus.Type = "Staff";
                unitOfWork._context.Accounts.Add(cus);
                unitOfWork.SaveChanges();
                return LocalRedirect(returnUrl);
            }catch (Exception ex)
            {
                error = ex.Message;
                return Page();
            }
        }
    }
}
