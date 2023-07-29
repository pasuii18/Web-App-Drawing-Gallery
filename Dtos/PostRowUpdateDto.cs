using System.ComponentModel.DataAnnotations;

namespace MinAPI.Dtos
{
    public class PostRowUpdateDto
    {
        [Required]
        [MaxLength(20)]
        public string? Nickname { get; set; }

        [Required]
        [MaxLength(20)]
        public string? NameOfPicture { get; set; }

        [Required]
        public string? Picture { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Date { get; set; }
    }
}
