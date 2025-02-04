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

public class DetailsModel : PageModel
{
    private readonly FitLogNeo.Data.ApplicationDbContext _context;

    public DetailsModel(FitLogNeo.Data.ApplicationDbContext context)
    {
        _context = context;
    }

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
}
