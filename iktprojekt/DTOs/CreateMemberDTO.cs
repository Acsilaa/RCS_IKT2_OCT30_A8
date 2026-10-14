using iktprojekt.Models;
using System.ComponentModel;

namespace iktprojekt.DTOs
{
    public class CreateMemberDTO
    {
        [DisplayName("Név")]
        public required string Name { get; set; }
        [DisplayName("E-Mail cím")]
        public string? Email { get; set; }
    }
}
