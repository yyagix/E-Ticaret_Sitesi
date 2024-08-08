using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true; // Özel karakterler
    options.Password.RequiredLength = 6; // Minimum þifre uzunluðu 6
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();  
app.UseSession();        

// Status code pages middleware
app.UseStatusCodePagesWithReExecute("/Home/AccessDenied");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.Run();


public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

        // Admin rolünü oluþtur
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new AppRole { Name = "Admin" });
        }

        // Admin kullanýcýsýný oluþtur
        var user = await userManager.FindByEmailAsync("admin@example.com");
        if (user == null)
        {
            user = new AppUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                FirstName = "AdminFirstName",
                LastName = "AdminLastName"
            };
            await userManager.CreateAsync(user, "Admin@1234"); // Admin Þifre
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}


