using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MySqlConnector;
using Proje.Areas.Identity.Data;
using System.Configuration;

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DBContextSampleConnection") ?? throw new InvalidOperationException("Connection string 'DBContextSampleConnection' not found.");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));


builder.Services.AddDbContext<DBContextSample>(options =>
{
    options.UseMySql(connectionString,serverVersion);
});

        builder.Services.AddDefaultIdentity<SampleUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DBContextSample>();

        builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
        // Add services to the container.
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        

        app.Run();

