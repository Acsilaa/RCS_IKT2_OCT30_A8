using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;

namespace iktprojekt.Pages.MemberPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        if (member is null)
        {
            return NotFound();
        }
        else
        {
            Member = member;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Members.FindAsync(id);
        if (member != null)
        {
            Member = member;
            _context.Members.Remove(Member);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
