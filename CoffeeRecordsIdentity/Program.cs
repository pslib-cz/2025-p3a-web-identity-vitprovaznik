using CoffeeRecordsIdentity.Data;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment() == false)
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    try 
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        bool existuje = roleManager.RoleExistsAsync("Admin").Result;
        if (existuje == false)
        {
            roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
        }

        var najityAdmin = userManager.FindByEmailAsync("admin@admin.cz").Result;
        if (najityAdmin == null)
        {
            var admin = new ApplicationUser { UserName = "admin@admin.cz", Email = "admin@admin.cz", Name = "Pan Admin" };
            userManager.CreateAsync(admin, "admin").Wait(); 
            userManager.AddToRoleAsync(admin, "Admin").Wait();
            
            int udelano = 1;
        }
    }
    catch 
    {
       
    }
}

app.Run();