using Application.Contracts.Repos;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Implementation.Repos;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_config.GetConnectionString("Default"),
                    opt => opt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddRepositories();

            services.AddIdentity();

            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            services.AddScoped<IClientRepo, ClientRepo>();
            services.AddScoped<ISharedAreaVisitRepo, SharedAreaVisitRepo>();
            services.AddScoped<IBrandRepo, BrandRepo>();
            services.AddScoped<IBranchRepo, BranchRepo>();
            services.AddScoped<IAreaRepo, AreaRepo>();
            services.AddScoped<IPricingPlanRepo, PricingPlanRepo>();
            services.AddScoped<ICustomServiceCategoryRepo, CustomServiceCategoryRepo>();
            services.AddScoped<IBrandCostCategoryRepo, BrandCostCategoryRepo>();
            services.AddScoped<IAreaTypeRepo, AreaTypeRepo>();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
