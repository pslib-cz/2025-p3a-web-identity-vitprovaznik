using CoffeeRecordsIdentity.Data;
using CoffeeRecordsIdentity.InputModels;
using CoffeeRecordsIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeRecordsIdentity.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public CoffeeCupIM Input { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            ApplicationUser prihlasenyUzivatel = await _userManager.GetUserAsync(User);
            
            if (prihlasenyUzivatel == null)
            {
                return Challenge();
            }

            CoffeeCup novySalek = new CoffeeCup();
            novySalek.MachineNo = Input.MachineNo;
            novySalek.UserName = prihlasenyUzivatel.UserName; 
            novySalek.UserId = prihlasenyUzivatel.Id; 
            novySalek.Created = DateTime.Now;

            _context.Cups.Add(novySalek);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}