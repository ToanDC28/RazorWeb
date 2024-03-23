using Eyeglasses_DoanCongToan.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public RegisterModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        [EmailAddress]
        [Required]
        public string email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [BindProperty]
        [Required]
        public string fullname { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid field");
                return Page();
            }
            var user = unitOfWork.StoreAccRepository.GetAll().Any(a => a.EmailAddress == email);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Account is existed");
                return Page();
            }
            var index = unitOfWork.StoreAccRepository.GetAll().Max(c => c.AccountId);
            unitOfWork.StoreAccRepository.Add(new Repo.Models.StoreAccount
            {
                AccountId = index + 1,
                AccountPassword = password,
                EmailAddress = email,
                FullName = fullname,
                Role = 4
            });
            unitOfWork.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
