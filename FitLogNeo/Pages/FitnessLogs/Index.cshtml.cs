using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FitLogNeo.Data;
using FitLogNeo.Models;
using Microsoft.AspNetCore.Identity;

namespace FitLogNeo.Pages.FitnessLogs;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<FitnessLog> FitnessLog { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);

        FitnessLog = await _context.FitnessLogs
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }
}
