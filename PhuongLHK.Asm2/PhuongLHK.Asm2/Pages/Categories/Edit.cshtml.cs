using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public EditModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        /*public IActionResult OnGet()
        {
           
        }*/
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("AccountID") == null)
            {
                return RedirectToPage("/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var category =  await unitOfWork._context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
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

            unitOfWork._context.Attach(Category).State = EntityState.Modified;

            try
            {
                await unitOfWork._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
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

        private bool CategoryExists(int id)
        {
          return (unitOfWork._context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
