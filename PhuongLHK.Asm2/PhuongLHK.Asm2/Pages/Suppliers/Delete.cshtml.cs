using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Suppliers
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public DeleteModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        [BindProperty]
      public Supplier Supplier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await unitOfWork._context.Suppliers.FirstOrDefaultAsync(m => m.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }
            else 
            {
                Supplier = supplier;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await unitOfWork._context.Suppliers.FindAsync(id);

            if (supplier != null)
            {
                Supplier = supplier;
                unitOfWork._context.Suppliers.Remove(Supplier);
                await unitOfWork._context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
