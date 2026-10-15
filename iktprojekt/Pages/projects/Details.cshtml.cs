using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

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

    public SelectList activeMembers { get; set; } = default!;
    public int completionRate { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int projectid, int? memberid)
    {

        var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(m => m.Id == projectid);
        
        if (project is null)
        {
            return NotFound();
        }
        else
        {
            var memberIds = project.Tasks.DistinctBy(t => t.MemberId).Where(t => t.MemberId != null).Select(t => t.MemberId).ToList();
            var _members = await _context.Members.Where(m => memberIds.Contains(m.Id)).ToListAsync();
            activeMembers = new SelectList(
                _members
                , "Id", "Name", memberid is not null ? memberid.ToString() : null
            );
            Project = project;
            var todos = project.Tasks;
            completionRate = (int)Math.Round((float)project.Tasks.Count(t => t.State == PTask.TodoState.Completed) / Math.Max(project.Tasks.Count(), 1) * 100,0);
            todos.ForEach(t => t.Member = _context.Members.FirstOrDefault(m => m.Id == t.MemberId));
            if(memberid is not null)
            {
                todos = todos.Where(t => t.MemberId == memberid).ToList();
            }
            CompletedTasks = todos.FindAll(t => t.State == PTask.TodoState.Completed);
            InProgressTasks = todos.FindAll(t => t.State == PTask.TodoState.InProgress);
            TodoTasks = todos.FindAll(t => t.State == PTask.TodoState.Todo);
        }

        return Page();
    }
}
