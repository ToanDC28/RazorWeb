﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public DetailsModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

      public Category Category { get; set; } = default!;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AccountID") == null)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await unitOfWork._context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }
    }
}
