using Microsoft.EntityFrameworkCore;
using PhuongLHK.Asm2.Repo.Models;
using PhuongLHK.Asm2.Repo.Repositories;

namespace PhuongLHK.Asm2.Web.Pages.Extentions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IRepository<TlAccount>, Repository<TlAccount>>();
            services.AddTransient<IRepository<Category>, Repository<Category>>();
            services.AddTransient<IRepository<Customer>, Repository<Customer>>();
            services.AddTransient<IRepository<Supplier>, Repository<Supplier>>();
            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<Order>, Repository<Order>>();
            services.AddTransient<IRepository<OrderDetail>, Repository<OrderDetail>>();
            services.AddTransient<UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDB(this IServiceCollection services)
        {
            services.AddDbContext<PRN_Assignment2Context>((option) =>
            {
                option.UseSqlServer(GetConnectionString());
            });
            return services;
        }
        private static string GetConnectionString()
        {
            IConfigurationRoot root = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return root.GetConnectionString("DefaultConnection");
        }
    }
}
