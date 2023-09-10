using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pustok.Database;
using Pustok.Services.Abstracts;
using Pustok.Services.Concretes;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Services
        builder.Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.Cookie.Name = "CeyhunIdentity";
                o.LoginPath = "/auth/login";
            });

        builder.Services
            .AddDbContext<PustokDbContext>(o =>
            {
                o.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            })
            .AddScoped<IUserService, UserService>()
            .AddSingleton<IFileService, ServerFileService>()
            .AddScoped<IEmailService, MailkitEmailService>()
            .AddHttpContextAccessor()
            .AddHttpClient();

        var app = builder.Build();

        //Middleware (Chain of responsibility)
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

        app.Run();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }

}
