using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using iktprojekt.DTOs;

namespace iktprojekt.Pages.PTaskPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public CreatePTaskDTO PTask { get; set; } = default!;
    public SelectList Members { get; set; } = default!;
    public Project Project { get; set; }

    public async Task<IActionResult> OnGetAsync(int prjid)
    {
        Project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == prjid);
        if (Project is null) return NotFound();
        Members = new SelectList(
            await _context.Members.ToListAsync(),
            "Id",
            "Name"
        );
        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int prjid)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == prjid);
        if (project is null) return NotFound();

        if (!ModelState.IsValid)
        {
            
            
            PTask = new CreatePTaskDTO
            {
                ProjectId = project.Id,
                State = PTask.State,
                Title = PTask.Title,
                Deadline = PTask.Deadline,
                MemberId = PTask.MemberId,
                Priority = PTask.Priority
            };
            Members = new SelectList(
            await _context.Members.ToListAsync(),
                "Id",
                "Name"
            );
            return Page();
        }
        if(PTask.Deadline > project.Deadline)
        {
            ModelState.AddModelError("Deadline", "A határidő nem lehet később, mint a projekt határideje.");
            return Page();
        }

        _context.Tasks.Add(new PTask { 
            ProjectId = project.Id,
            Title = PTask.Title,
            State = PTask.State,
            Priority = PTask.Priority,
            Deadline = PTask.Deadline,
            MemberId = PTask.MemberId
        });
        await _context.SaveChangesAsync();

        return Redirect($"/project/{prjid}/details");
    }
}
