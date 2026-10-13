
using iktprojekt.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static iktprojekt.Models.PTask;

namespace iktprojekt.DTOs
{
    public class CreatePTaskDTO
    {
        [DisplayName("Cím")]
        [Required]
        public required string Title { get; set; }
        [DisplayName("Állapot")]
        [Required]
        public required TodoState State { get; set; } = TodoState.Todo;
        [DisplayName("Prioritás")]
        public TodoPriority? Priority { get; set; }
        [DisplayName("Határidő")]
        public DateTime? Deadline { get; set; }

        // FK
        [DisplayName("Tag")]
        public int? MemberId { get; set; }
        [DisplayName("Projekt")]
        public required int ProjectId { get; set; }
    }
}
