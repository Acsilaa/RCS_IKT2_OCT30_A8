using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iktprojekt.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Név")]
        public required string Name { get; set; }
        [DisplayName("E-Mail cím")]
        public string? Email { get; set; }


        // Nav prop
        [DisplayName("Teendők")]
        public List<PTask> Tasks { get; set; } = new();

        public override string ToString()
        {
            return this.Name;
        }
    }
}
