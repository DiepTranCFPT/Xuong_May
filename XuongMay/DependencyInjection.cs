using Microsoft.EntityFrameworkCore;
using XuongMay.Models.Entity;

namespace XuongMay
{
    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            // Register your DbContext (XuongMayContext)
            services.AddDbContext<XuongMayContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add other services as needed

        }
    }
}
