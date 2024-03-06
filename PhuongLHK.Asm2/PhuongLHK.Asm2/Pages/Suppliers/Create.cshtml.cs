using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Suppliers
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public CreateModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Supplier Supplier { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            if (unitOfWork._context.Suppliers.Any(a => a.CompanyName == Supplier.CompanyName && a.Address == Supplier.Address))
            {
                ModelState.AddModelError(string.Empty, "Company has existed");
                return Page();
            }
            var comID = unitOfWork._context.Suppliers.Max(c => c.SupplierId);

            Supplier.SupplierId = comID + 1;

            unitOfWork._context.Suppliers.Add(Supplier);
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
