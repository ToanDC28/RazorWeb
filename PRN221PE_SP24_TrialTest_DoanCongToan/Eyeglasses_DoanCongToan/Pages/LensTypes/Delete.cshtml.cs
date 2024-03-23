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
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public DeleteModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
        public int role { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lenstype = unitOfWork.lenTypeRepository.GetAll().Where(a => a.LensTypeId == id).FirstOrDefault();

            if (lenstype != null)
            {
                unitOfWork.lenTypeRepository.Delete(lenstype);
                unitOfWork.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
