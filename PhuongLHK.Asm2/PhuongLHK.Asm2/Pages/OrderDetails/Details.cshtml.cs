using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;

namespace PhuongLHK.Asm2.Web.Pages.OrderDetails
{
    public class DetailsModel : PageModel
    {
        private readonly PhuongLHK.Asm2.Repo.Models.PRN_Assignment2Context _context;

        public DetailsModel(PhuongLHK.Asm2.Repo.Models.PRN_Assignment2Context context)
        {
            _context = context;
        }

      public OrderDetail OrderDetail { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetail = orderdetail;
            }
            return Page();
        }
    }
}
