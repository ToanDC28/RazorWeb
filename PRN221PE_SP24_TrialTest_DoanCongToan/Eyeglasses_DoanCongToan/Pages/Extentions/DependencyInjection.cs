using Eyeglasses_DoanCongToan.Repo.Models;
using Eyeglasses_DoanCongToan.Repo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eyeglasses_DoanCongToan.Web.Pages.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Eyeglass>, Repository<Eyeglass>>();
            services.AddTransient<IRepository<LensType>, Repository<LensType>>();
            services.AddTransient<IRepository<StoreAccount>, Repository<StoreAccount>>();
            services.AddTransient<UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDB(this IServiceCollection services)
        {
            services.AddDbContext<Eyeglasses2024DBContext>((option) =>
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
