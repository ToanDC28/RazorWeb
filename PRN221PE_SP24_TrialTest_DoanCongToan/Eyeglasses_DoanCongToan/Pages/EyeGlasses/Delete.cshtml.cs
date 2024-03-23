using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.EyeGlasses
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public DeleteModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int role { get; set; }

        [BindProperty]
      public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            var eyeglass = unitOfWork.eyeGlassRepository.GetAll().FirstOrDefault(p => p.EyeglassesId == id.Value);

            if (eyeglass == null)
            {
                return NotFound();
            }
            else 
            {
                Eyeglass = eyeglass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eyeglass = await unitOfWork.eyeGlassRepository.GetByIdAsync(id.Value);

            if (eyeglass != null)
            {
                Eyeglass = eyeglass;
                unitOfWork.eyeGlassRepository.Delete(Eyeglass);
                await   unitOfWork.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
