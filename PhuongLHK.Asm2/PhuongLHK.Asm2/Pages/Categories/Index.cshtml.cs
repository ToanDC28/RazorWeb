using System;
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
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public IndexModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public IList<Category> Category { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("AccountID") == null)
            {
                return RedirectToPage("/Account/Login");
            }
            Category = unitOfWork.categoryRepository.GetAll().ToList();
            return Page();
        }
    }
}
