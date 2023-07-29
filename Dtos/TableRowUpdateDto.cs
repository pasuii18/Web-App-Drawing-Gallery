using System.ComponentModel.DataAnnotations;

namespace MinAPI.Dtos
{
    public class TableRowUpdateDto
    {
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
