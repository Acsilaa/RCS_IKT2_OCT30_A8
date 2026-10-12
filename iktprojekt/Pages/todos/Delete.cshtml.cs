using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;

namespace iktprojekt.Pages.PTaskPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PTask PTask { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ptask = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);
        if (ptask is null)
        {
            return NotFound();
        }
        else
        {
            PTask = ptask;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ptask = await _context.Tasks.FindAsync(id);
        if (ptask != null)
        {
            PTask = ptask;
            _context.Tasks.Remove(PTask);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
