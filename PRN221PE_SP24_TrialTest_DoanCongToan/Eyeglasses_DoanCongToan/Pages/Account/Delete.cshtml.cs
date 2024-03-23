using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;
using System.Data;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public DeleteModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /*[BindProperty]
      public StoreAccount StoreAccount { get; set; } = default!;*/

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == userid);
            int role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            var storeaccount = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == id.Value);

            if (storeaccount == null)
            {
                return NotFound();
            }
            else 
            {
                unitOfWork.StoreAccRepository.Delete(storeaccount);
            }
            return Page();
        }

        
    }
}
