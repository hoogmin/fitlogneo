using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitLogNeo.Data;
using FitLogNeo.Models;
using Microsoft.AspNetCore.Identity;

namespace FitLogNeo.Pages.FitnessLogs;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public FitnessLog FitnessLog { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {        

        if (!ModelState.IsValid)
        {
            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;

                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
                }
            }

            return Page();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }

        FitnessLog.UserId = user.Id;

        _context.FitnessLogs.Add(FitnessLog);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
