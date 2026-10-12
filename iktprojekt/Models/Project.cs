using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iktprojekt.Models
{
    public class Project
    {
        public int Id { get; set;  }
        [Required]
        [DisplayName("Név")]
        public required string Name { get; set; }
        [DisplayName("Leírás")]
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Határidő")]
        public DateTime? Deadline {  get; set; }

        // Nav props
        public List<PTask> Tasks = new();
    }
}
