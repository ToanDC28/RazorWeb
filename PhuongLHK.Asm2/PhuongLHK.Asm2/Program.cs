using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;
using PhuongLHK.Asm2.Web.Pages.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUnitOfWork();
builder.Services.AddDB();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseSession();
app.Run();
