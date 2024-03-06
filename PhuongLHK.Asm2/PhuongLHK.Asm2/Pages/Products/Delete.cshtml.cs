using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public DeleteModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await unitOfWork._context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await unitOfWork._context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                unitOfWork._context.Products.Remove(Product);
                await unitOfWork._context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
