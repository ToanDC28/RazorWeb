using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        public CreateModel(UnitOfWork unit)
        {
            this.unitOfWork = unit;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(unitOfWork._context.Categories, "CategoryId", "CategoryName");
        ViewData["SupplierId"] = new SelectList(unitOfWork._context.Suppliers, "SupplierId", "CompanyName");
            return Page();
        }

        [BindProperty]
        public string ProductName { get; set; } = default!;
        [BindProperty]
        public string SupplierId { get; set; } = default!;
        [BindProperty]
        public string CategoryId { get; set; } = default!;

        [BindProperty]
        public string QuantityPerUnit { get; set; } = default!;
        [BindProperty]
        public string UnitPrice { get; set; } = default!;
        [BindProperty]
        public IFormFile ProductImage { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          /*if (!ModelState.IsValid)
            {
                return Page();
            }*/
            Product Product = new Product();
            if (unitOfWork._context.Products.Any(a => a.ProductName == ProductName))
            {
                ModelState.AddModelError(string.Empty, "Product has existed");
                return Page();
            }
            var proID = unitOfWork._context.Products.Max(c => c.ProductId);

            Product.ProductId = proID + 1;
            Product.ProductName = ProductName;
            Product.UnitPrice = decimal.Parse(UnitPrice);
            Product.QuantityPerUnit = QuantityPerUnit;
            Product.SupplierId = int.Parse(SupplierId);
            Product.CategoryId = int.Parse(CategoryId);
            if(ProductImage != null)
{
                using (var memoryStream = new MemoryStream())
                {
                    await ProductImage.CopyToAsync(memoryStream);
                    Product.ProductImage = memoryStream.ToArray();
                }
            }

            unitOfWork._context.Products.Add(Product);
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
