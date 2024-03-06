using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.LensTypes
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public IndexModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<LensType> LensType { get;set; } = default!;
        public int role { get; set; }
        public IActionResult OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role == 4)
            {
                return RedirectToPage("/Account/Login");
            }
            if (unitOfWork._context.LensTypes != null)
            {
                LensType = unitOfWork._context.LensTypes.ToList();
            }
            return Page();
        }
    }
}
