using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public ICollection<CoffeeCup> Cups { get; set; }
}