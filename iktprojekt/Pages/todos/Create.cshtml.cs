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

    public SelectList Projects { get; set; } = default!;
    public SelectList States { get; set; } = default!;
    public SelectList Members { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Projects = new SelectList(
            await _context.Projects.ToListAsync(),
            "Id",
            "Name"
        );
        States = new SelectList(
            Enum.GetValues<PTask.TodoState>()
        );
        Members = new SelectList(
            await _context.Members.ToListAsync(),
            "Id",
            "Name"
        );
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Projects = new SelectList(
            await _context.Projects.ToListAsync(),
                "Id",
                "Name"
            );
            States = new SelectList(
                Enum.GetValues<PTask.TodoState>()
            );
            Members = new SelectList(
            await _context.Members.ToListAsync(),
                "Id",
                "Name"
            );
            return Page();
        }

        _context.Tasks.Add(new PTask { 
            ProjectId = PTask.ProjectId,
            Title = PTask.Title,
            State = PTask.State,
            Priority = PTask.Priority,
            Deadline = PTask.Deadline,
            MemberId = PTask.MemberId
        });
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
