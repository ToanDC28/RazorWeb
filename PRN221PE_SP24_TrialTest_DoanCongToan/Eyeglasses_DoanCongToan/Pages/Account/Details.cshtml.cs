﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;

namespace Eyeglasses_DoanCongToan.Web.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        public DetailsModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public int role { get; set; }
        public StoreAccount StoreAccount { get; set; } = default!; 

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
            var storeaccount = unitOfWork.StoreAccRepository.GetAll().FirstOrDefault(p => p.AccountId == id.Value);
            if (storeaccount == null)
            {
                return NotFound();
            }
            else 
            {
                StoreAccount = storeaccount;
            }
            return Page();
        }
    }
}
