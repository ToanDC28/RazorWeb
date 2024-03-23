using Eyeglasses_DoanCongToan.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public LoginModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [BindProperty]
        [EmailAddress]
        [Required] 
        public string email {  get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string error { get; set; }
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
            if (!ModelState.IsValid) {
                error = "All field is required";
                return Page();
            }
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.EmailAddress == email && p.AccountPassword == password);
            if (user == null)
            {
                error = "User is in valid";
                return Page();
            }
            HttpContext.Session.SetString("userID", user.AccountId.ToString());
            return RedirectToPage("/Index");
        }
    }
}
