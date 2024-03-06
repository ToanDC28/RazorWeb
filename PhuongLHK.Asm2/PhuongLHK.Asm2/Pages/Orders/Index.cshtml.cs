using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public IndexModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {

            Order = await unitOfWork._context.Orders
                .Include(o => o.Customer).ToListAsync();
        }
    }
}
