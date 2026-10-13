using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iktprojekt.Models
{
    public class PTask
    {
        public enum TodoState
        {
            [Display(Name ="Teendő")]
            Todo = 1,
            [Display(Name = "Folyamatban")]
            InProgress = 2,
            [Display(Name = "Kész")]
            Completed = 3,
        }
        public enum TodoPriority
        {
            [Display(Name = "Alacsony")]
            Low = 1,

            [Display(Name = "Közepes")]
            Medium = 2,

            [Display(Name = "Magas")]
            High = 3
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
        [DisplayName("Tag")]
        public int? MemberId { get; set; }
        [DisplayName("Projekt")]
        public required int ProjectId { get; set; }

        //Nav prop
        [DisplayName("Tag")]
        public Member? Member { get; set; } = null!;

        [DisplayName("Projekt")]
        public Project Project { get; set; } = null!;
    }
}
