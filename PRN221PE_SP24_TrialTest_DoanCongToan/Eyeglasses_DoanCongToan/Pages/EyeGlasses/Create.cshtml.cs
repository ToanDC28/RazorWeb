using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.EyeGlasses
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public CreateModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int role { get; set; }

        public IActionResult OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            ViewData["LensTypeId"] = new SelectList(unitOfWork._context.LensTypes, "LensTypeId", "LensTypeName");
            return Page();
        }

        [BindProperty]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity {  get; set; }
        public string LensTypeId { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            int index = unitOfWork._context.Eyeglasses.Max(p => p.EyeglassesId);
            if(Quantity > 999 || Quantity < 0)
            {
                ModelState.AddModelError(string.Empty, "Quantity is invalid");
            }
            unitOfWork._context.Eyeglasses.Add(new Eyeglass
            {
                EyeglassesId = index + 1,
                CreatedDate = DateTime.Now,
                EyeglassesDescription = Description,
                EyeglassesName = Name,
                FrameColor = Color,
                LensTypeId = LensTypeId
            });
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
