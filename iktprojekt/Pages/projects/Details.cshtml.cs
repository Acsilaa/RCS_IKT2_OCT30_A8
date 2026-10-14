using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;

namespace iktprojekt.Pages.ProjectPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Project Project { get; set; } = default!;
    public List<PTask> CompletedTasks { get; set; } = new();
    public List<PTask> InProgressTasks { get; set; } = new();
    public List<PTask> TodoTasks { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int projectid)
    {

        var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(m => m.Id == projectid);
        if (project is null)
        {
            return NotFound();
        }
        else
        {
            Project = project;
            var todos = project.Tasks;
            todos.ForEach(t => t.Member = _context.Members.FirstOrDefault(m => m.Id == t.MemberId));
            CompletedTasks = todos.FindAll(t => t.State == PTask.TodoState.Completed);
            InProgressTasks = todos.FindAll(t => t.State == PTask.TodoState.InProgress);
            TodoTasks = todos.FindAll(t => t.State == PTask.TodoState.Todo);
        }

        return Page();
    }
}
