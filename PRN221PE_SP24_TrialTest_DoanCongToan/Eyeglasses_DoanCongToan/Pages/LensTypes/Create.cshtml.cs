using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Eyeglasses_DoanCongToan.Web.Pages.LensTypes
{
    public class CreateModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public CreateModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }
        public int role { get; set; }

        [BindProperty]
        [Required]
        public string LensTypeName { get; set; }
        [BindProperty]
        [Required]
        public string Description { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var index = unitOfWork._context.LensTypes.Max(a => a.LensTypeId);
            unitOfWork._context.LensTypes.Add(new LensType
            {
                LensTypeId = index + 1,
                LensTypeName = LensTypeName,
                LensTypeDescription = Description,
                IsPrescription = Description.IsNullOrEmpty()
            });
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
