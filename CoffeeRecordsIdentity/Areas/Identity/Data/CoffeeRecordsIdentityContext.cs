using CoffeeRecordsIdentity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeRecordsIdentity.Data;

public class CoffeeRecordsIdentityContext : IdentityDbContext<CoffeeRecordsIdentityUser>
{
    public CoffeeRecordsIdentityContext(DbContextOptions<CoffeeRecordsIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
