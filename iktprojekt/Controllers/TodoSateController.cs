using iktprojekt.Data;
using iktprojekt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace iktprojekt.Controllers
{
    public class ProgressStateRequest
    {
        public int todoId { get; set; }
    }

    [ApiController]
    [Route("todo")]
    public class TodoSateController : Controller
    {

        private readonly ApplicationDbContext _context;
        public TodoSateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("progressstate")]
        public async Task<IActionResult> ProgressState([FromBody] ProgressStateRequest request)
        {
            int id = request.todoId;
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if(task is null)
            {
                return NotFound();
            }
            if(task.State == PTask.TodoState.Todo)
            {
                task.State = PTask.TodoState.InProgress;
            }
            else if (task.State == PTask.TodoState.InProgress)
            {
                task.State = PTask.TodoState.Completed;
            }
            _context.Attach(task).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(task);
        }
    }
}
