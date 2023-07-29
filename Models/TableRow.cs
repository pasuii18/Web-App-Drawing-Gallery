using System.ComponentModel.DataAnnotations;

namespace MinAPI.Models
{
    public class TableRow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Nickname { get; set; }

        [Required]
        [MaxLength(70)]
        public string? Login { get; set; }

        [Required]
        [MaxLength(70)]
        public string? Password { get; set; }
    }
}
