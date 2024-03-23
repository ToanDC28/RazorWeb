using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public IndexModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IList<StoreAccount> Account { get; set; } = default!;
        public int role { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                int id = int.Parse(HttpContext.Session.GetString("userID"));
                var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == id);
                if (user.Role == 4)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    role = user.Role.Value;
                    Account = unitOfWork.StoreAccRepository.GetAll().ToList();
                    return Page();
                }

            }
            return RedirectToPage("/Account/Login");
        }
    }
}
