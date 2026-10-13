using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iktprojekt.Models;
using iktprojekt.Data;
using System.Diagnostics;

namespace iktprojekt.Pages.MemberPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Member Member { get; set; } = default!;
    public List<string> Tasks { get; set; } = new List<string>();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Members.Include(m => m.Tasks).FirstOrDefaultAsync(m => m.Id == id);
        if (member is null)
        {
            return NotFound();
        }
        else
        {
            Member = member;
            Tasks = member.Tasks.Select(t => t.Title).ToList();
        }

        return Page();
    }
}
