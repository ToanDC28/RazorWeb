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

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
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
        public string password {  get; set; }
        [BindProperty]
        [Required]
        public string fullname { get; set; }
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [BindProperty]
        [Required]
        public int userrole { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            
            int index = unitOfWork._context.StoreAccounts.Max(a => a.AccountId);
            unitOfWork._context.StoreAccounts.Add(new StoreAccount
            {
                AccountId = index + 1,
                AccountPassword = password,
                EmailAddress = email,
                FullName = fullname,
                Role = userrole
            });
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
