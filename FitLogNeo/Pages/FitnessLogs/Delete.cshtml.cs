using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitLogNeo.Data;
using FitLogNeo.Models;

namespace FitLogNeo.Pages.FitnessLogs;

public class DeleteModel : PageModel
{
    private readonly FitLogNeo.Data.ApplicationDbContext _context;

    public DeleteModel(FitLogNeo.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public FitnessLog FitnessLog { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fitnesslog = await _context.FitnessLogs.FirstOrDefaultAsync(m => m.Id == id);

        if (fitnesslog == null)
        {
            return NotFound();
        }
        else
        {
            FitnessLog = fitnesslog;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var fitnesslog = await _context.FitnessLogs.FindAsync(id);
        if (fitnesslog != null)
        {
            FitnessLog = fitnesslog;
            _context.FitnessLogs.Remove(FitnessLog);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
