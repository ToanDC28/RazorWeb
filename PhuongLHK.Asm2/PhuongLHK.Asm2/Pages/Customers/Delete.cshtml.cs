using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public DeleteModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("AccountID") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var customer = await unitOfWork._context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await unitOfWork._context.Customers.FindAsync(id);

            if (customer != null)
            {
                Customer = customer;
                unitOfWork._context.Customers.Remove(Customer);
                await unitOfWork.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
