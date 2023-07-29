using System.ComponentModel.DataAnnotations;

namespace MinAPI.Dtos
{
    public class TableRowReadDto
    {
        public int Id { get; set; }

        public string? Nickname { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }
    }
}
