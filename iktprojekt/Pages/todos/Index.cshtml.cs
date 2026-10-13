using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;

namespace iktprojekt.Pages.PTaskPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<PTask> PTask { get; set; } = default!;

    public async Task OnGetAsync()
    {
        PTask = await _context.Tasks.Include(t => t.Member).Include(t => t.Project).ToListAsync();
    }
}
