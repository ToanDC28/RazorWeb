using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.EyeGlasses
{
    public class IndexModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public IndexModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int role { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 4;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        [BindProperty]
        public string SearchString {  get; set; }
        public IList<Eyeglass> Eyeglass { get;set; } = default!;

        public IActionResult OnGet()
        {
            int userid = int.Parse(HttpContext.Session.GetString("userID"));
            var user = unitOfWork._context.StoreAccounts.FirstOrDefault(a => a.AccountId == userid);
            role = user.Role.Value;
            if (HttpContext.Session.GetString("userID") == null || role == 4)
            {
                return RedirectToPage("/Account/Login");
            }
            Count = unitOfWork.eyeGlassRepository.GetAll().Count();
            Eyeglass = unitOfWork.eyeGlassRepository.GetAll().Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            
            return Page();
        }
        public IActionResult OnPost()
        {
            var eyes = unitOfWork._context.Eyeglasses.Where(s => s.EyeglassesDescription.Contains(SearchString)).Skip((CurrentPage - 1) * PageSize).Take(PageSize)
                .Include(e => e.LensType);
            if (!eyes.Any())
            {
                eyes = unitOfWork._context.Eyeglasses.Where(s => s.Price == decimal.Parse(SearchString)).Skip((CurrentPage - 1) * PageSize).Take(PageSize)
                .Include(e => e.LensType);
            }
            Count = eyes.Count();
            Eyeglass = eyes.ToList();
            return Page();
        }
    }
}
