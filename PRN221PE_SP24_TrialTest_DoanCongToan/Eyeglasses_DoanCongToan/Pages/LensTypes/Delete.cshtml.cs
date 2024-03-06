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
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
        public int role { get; set; }

        /*public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lenstype = await unitOfWork._context.LensTypes.FirstOrDefaultAsync(m => m.LensTypeId == id);

            if (lenstype == null)
            {
                return NotFound();
            }
            else 
            {
                LensType = lenstype;
            }
            return Page();
        }*/

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lenstype = await unitOfWork._context.LensTypes.FindAsync(id);

            if (lenstype != null)
            {
                unitOfWork._context.LensTypes.Remove(lenstype);
                await unitOfWork._context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
