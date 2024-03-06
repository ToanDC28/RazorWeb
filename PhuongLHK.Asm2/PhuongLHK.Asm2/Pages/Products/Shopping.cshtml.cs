using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Products
{
    public class ShoppingModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public ShoppingModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }
        public IList<Product> Product { get; set; } = default!;

        public void OnGet()
        {
            Product = unitOfWork._context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToList();
        }
    }
}
