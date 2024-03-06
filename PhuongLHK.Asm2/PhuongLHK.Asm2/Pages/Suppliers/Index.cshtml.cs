using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;

namespace PhuongLHK.Asm2.Web.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly PhuongLHK.Asm2.Repo.Models.PRN_Assignment2Context _context;

        public IndexModel(PhuongLHK.Asm2.Repo.Models.PRN_Assignment2Context context)
        {
            _context = context;
        }

        public IList<Supplier> Supplier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Suppliers != null)
            {
                Supplier = await _context.Suppliers.ToListAsync();
            }
        }
    }
}
