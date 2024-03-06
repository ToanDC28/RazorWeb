using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;
using static NuGet.Packaging.PackagingConstants;

namespace PhuongLHK.Asm2.Web.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private UnitOfWork unitOfWork;

        public CreateModel(UnitOfWork unit)
        {
            unitOfWork = unit;
        }

        /*[BindProperty]
        public Order Order { get; set; } = default!;*/

        public IActionResult OnGet(int? id)
        {
            if(HttpContext.Session.GetString("CustomerID") != null && id.HasValue)
            {
                if(HttpContext.Session.GetString("OrderDetail") != null)
                {
                    var data = JsonConvert.DeserializeObject<List<OrderDetail>>(HttpContext.Session.GetString("OrderDetail"));
                    var orderID = unitOfWork._context.Orders.Max(c => c.CustomerId);
                    data.Add(new OrderDetail
                    {
                        OrderId = orderID.Value + 1,
                        ProductId = id.Value,
                        UnitPrice = unitOfWork._context.Products.FirstOrDefault(a => a.ProductId == id.Value).UnitPrice,
                        Quantity = 1
                    }) ;
                    HttpContext.Session.Remove("OrderDetail");
                    HttpContext.Session.SetString("OrderDetail", JsonConvert.SerializeObject(data));

                }
                else
                {
                    List<OrderDetail> orders = new List<OrderDetail>();
                    var orderID = unitOfWork._context.Orders.Max(c => c.CustomerId);
                    orders.Add(new OrderDetail
                    {
                        OrderId = orderID.Value + 1,
                        ProductId = id.Value,
                        UnitPrice = unitOfWork._context.Products.FirstOrDefault(a => a.ProductId == id.Value).UnitPrice,
                        Quantity = 1
                    });
                    HttpContext.Session.SetString("OrderDetail", JsonConvert.SerializeObject(orders));
                }

                
            }
            return RedirectToPage("~/");
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            /*unitOfWork._context.Orders.Add(Order);*/
            await unitOfWork._context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
