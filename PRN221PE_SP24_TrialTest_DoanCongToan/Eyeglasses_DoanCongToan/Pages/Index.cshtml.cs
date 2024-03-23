using Eyeglasses_DoanCongToan.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_DoanCongToan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly UnitOfWork unitOfWork;
        
        public IndexModel(ILogger<IndexModel> logger, UnitOfWork unit)
        {
            _logger = logger;
            this.unitOfWork = unit;
        }
        public int role { get; set; }
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("userID") == null)
            {
                return RedirectToPage("/Account/Login");
            }
            int id = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == id);
            role = user.Role.Value;
            return Page();
        }
    }
}
