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

namespace PhuongLHK.Asm2.Web.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public CreateModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task OnGetAsync(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

        }
    }
}
