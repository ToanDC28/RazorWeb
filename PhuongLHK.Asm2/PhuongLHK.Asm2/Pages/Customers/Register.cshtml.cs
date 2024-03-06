using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;
using System.Dynamic;

namespace PhuongLHK.Asm2.Web.Pages.Customers
{
    public class RegisterModel : PageModel
    {
        private UnitOfWork unitOfWork;

        public RegisterModel(UnitOfWork unit)
        {
            unitOfWork = unit;
        }
        [BindProperty]
        public string ContactName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        public string error { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                return RedirectToPage("./Login");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("./Login");

            try
            {
                Customer cus = new Customer();
                if (unitOfWork._context.Customers.Any(a => a.Phone == Phone))
                {
                    error = Phone + " has existed";
                    return Page();
                }
                var cusID = unitOfWork._context.Customers.Max(c => c.CustomerId);
                cus.CustomerId = cusID + 1;
                cus.Address = Address;
                cus.Phone = Phone;
                cus.ContactName = ContactName;
                cus.Password = Password;
                unitOfWork._context.Customers.Add(cus);
                unitOfWork.SaveChanges();
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return Redirect(returnUrl);
            }
        }
    }
}
