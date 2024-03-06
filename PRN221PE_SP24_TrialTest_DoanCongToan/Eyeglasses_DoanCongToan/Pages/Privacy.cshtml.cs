using Eyeglasses_DoanCongToan.Repo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eyeglasses_DoanCongToan.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public PrivacyModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public int role {  get; set; }
        public void OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
        }
    }

}
