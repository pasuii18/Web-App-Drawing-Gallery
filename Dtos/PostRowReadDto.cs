using System.ComponentModel.DataAnnotations;

namespace MinAPI.Dtos
{
    public class PostRowReadDto
    {
        public int Id { get; set; }
        public string? Nickname { get; set; }
        
        public string? NameOfPicture { get; set; }

        public string? Picture { get; set; }

        public string? Date { get; set; }
    }
}
