using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.LensTypes
{
    public class EditModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public EditModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int role { get; set; }

        [BindProperty]
        public LensType LensType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var lenstype =  await unitOfWork._context.LensTypes.FirstOrDefaultAsync(m => m.LensTypeId == id);
            if (lenstype == null)
            {
                return NotFound();
            }
            LensType = lenstype;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork._context.Attach(LensType).State = EntityState.Modified;

            try
            {
                await unitOfWork._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LensTypeExists(LensType.LensTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LensTypeExists(string id)
        {
          return (unitOfWork._context.LensTypes?.Any(e => e.LensTypeId == id)).GetValueOrDefault();
        }
    }
}
