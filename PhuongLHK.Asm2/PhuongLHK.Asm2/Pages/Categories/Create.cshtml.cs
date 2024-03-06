using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public CreateModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet()
        {
           if(HttpContext.Session.GetString("AccountID") == null)
            {
                return RedirectToPage("/Account/Login");
            }
           return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Category == null)
            {
                return Page();
            }
            if (unitOfWork._context.Categories.Any(a => a.CategoryName == Category.CategoryName))
            {
                ModelState.AddModelError(string.Empty, "Category name has existed");
                return Page();
            }
            var cateID = unitOfWork._context.Categories.Max(c => c.CategoryId);

            Category.CategoryId = cateID + 1;
            unitOfWork._context.Categories.Add(Category);
            await unitOfWork.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
