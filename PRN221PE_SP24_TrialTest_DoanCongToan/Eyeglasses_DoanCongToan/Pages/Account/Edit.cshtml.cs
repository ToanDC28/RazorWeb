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

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
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
        public StoreAccount StoreAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }

            var storeaccount =  await unitOfWork._context.StoreAccounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (storeaccount == null)
            {
                return NotFound();
            }
            
            StoreAccount = storeaccount;
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

            unitOfWork._context.Attach(StoreAccount).State = EntityState.Modified;

            try
            {
                await unitOfWork._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreAccountExists(StoreAccount.AccountId))
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

        private bool StoreAccountExists(int id)
        {
          return (unitOfWork._context.StoreAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
