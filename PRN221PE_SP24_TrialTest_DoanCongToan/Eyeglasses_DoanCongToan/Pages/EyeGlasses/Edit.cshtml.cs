using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.EyeGlasses
{
    public class EditModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public EditModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int role { get; set; }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role != 1)
            {
                return RedirectToPage("/Account/Login");
            }

            var eyeglass = unitOfWork.eyeGlassRepository.GetAll().FirstOrDefault(p => p.EyeglassesId == id.Value);
            if (eyeglass == null)
            {
                return NotFound();
            }
            Eyeglass = eyeglass;
            ViewData["LensTypeId"] = new SelectList(unitOfWork.lenTypeRepository.GetAll(), "LensTypeId", "LensTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.eyeGlassRepository.Update(Eyeglass);

            try
            {
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeglassExists(Eyeglass.EyeglassesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EyeglassExists(int id)
        {
            return (unitOfWork.eyeGlassRepository.GetAll().Any(e => e.EyeglassesId == id));
        }
    }
}
