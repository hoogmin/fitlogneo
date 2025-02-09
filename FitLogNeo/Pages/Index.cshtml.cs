using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitLogNeo.Data;
using FitLogNeo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitLogNeo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public bool IsAuthenticated { get; private set; }
    public IdentityUser? CurrentUser { get; private set; }
    public int TotalLogs { get; private set; }
    public int LastWeekLogs { get; private set; }
    public FitnessLog? MostRecentLog { get; private set; }

    public async Task OnGetAsync()
    {
        IsAuthenticated = User.Identity.IsAuthenticated;

        if (IsAuthenticated)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser is not null)
            {
                var logs = _context.FitnessLogs.Where(log => log.UserId == CurrentUser.Id);

                TotalLogs = await logs.CountAsync();
                LastWeekLogs = await logs.Where(log => log.Date >= DateTime.UtcNow.AddDays(-7)).CountAsync();
                MostRecentLog = await logs.OrderByDescending(log => log.Date).FirstOrDefaultAsync();
            }
        }
    }
}
