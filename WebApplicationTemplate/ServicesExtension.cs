using Microsoft.EntityFrameworkCore;
using WebApplicationTemplate.Areas.Identity.Data;
using WebApplicationTemplate.Data;
using WebApplicationTemplate.Mappings;

namespace WebApplicationTemplate
{
    public static class ServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddRazorPages();

            // ApplicationContext
            services.AddDbContext<ApplicationContext>(options =>
                  options.UseSqlServer(config.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));

            //IdentityContext
            services.AddDbContext<IdentityContext>(options =>
                   options.UseSqlServer(config.GetConnectionString("IdentityContext") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
