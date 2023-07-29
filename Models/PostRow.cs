using System.ComponentModel.DataAnnotations;

namespace MinAPI.Models
{
    public class PostRow
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Nickname { get; set; }

        [Required]
        [MaxLength(20)]
        public string? NameOfPicture { get; set; }

        [Required]
        public string? Picture { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Date { get; set; }
    }
}
