using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iktprojekt.Models
{
    public class PTask
    {
        public enum TodoState
        {
            Todo,
            InProgress,
            Completed,
        }
        public enum TodoPriority
        {
            Low = 1,
            Medium = 2,
            High = 3,
        }

        public int Id { get; set; }
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
        public int? MemberId { get; set; }
        [Required]
        public required int ProjectId { get; set; }

        //Nav prop
        public Member? Member { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}
