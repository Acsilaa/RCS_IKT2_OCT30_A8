using iktprojekt.Data;
using iktprojekt.DTOs;
using iktprojekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace iktprojekt.Pages.PTaskPages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public EditPTaskDTO PTask { get; set; } = default!;
    public SelectList Members { get; set; } = default!;
    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public async Task<IActionResult> OnGetAsync(int todoid)
    {
        var ptask = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == todoid);
        if (ptask is null) return NotFound();

        PTask = new EditPTaskDTO { 
            ProjectId = ptask.ProjectId, 
            State = ptask.State, 
            Title = ptask.Title , 
            Deadline = ptask.Deadline, 
            MemberId = ptask.MemberId ,
            Id = ptask.Id, 
            Member = ptask.Member, 
            Priority = ptask.Priority
        };

        ProjectId = ptask.ProjectId; // or appropriate FK property
        Project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == ProjectId);

        Members = new SelectList(await _context.Members.ToListAsync(), "Id", "Name");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int todoid)
    {
        ProjectId = PTask.ProjectId; // or appropriate FK property
        var _ptask = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == todoid);
        Project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == _ptask.ProjectId);
        if (!ModelState.IsValid)
        {
            Members = new SelectList(
            await _context.Members.ToListAsync(),
                "Id",
                "Name"
            );
            Console.WriteLine(string.Join(", ", ModelState.SelectMany(e => e.Value.Errors.Select(err => $"{e.Key}: {err.ErrorMessage}"))));
            return Page();
       
            
        }
        if (PTask.Deadline > Project.Deadline)
        {
            ModelState.AddModelError("ProjectId", "A határidő nem lehet később, mint a projekt határideje.");
            Members = new SelectList(
            await _context.Members.ToListAsync(),
                "Id",
                "Name"
            );
            return Page();
        }
        _ptask.Title = PTask.Title;
        _ptask.State = PTask.State;
        _ptask.Priority = PTask.Priority;
        _ptask.Deadline = PTask.Deadline;
        _ptask.MemberId = PTask.MemberId;
        _ptask.ProjectId = PTask.ProjectId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PTaskExists(PTask.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Redirect($"/project/{ProjectId}/details");
    }

    private bool PTaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}
