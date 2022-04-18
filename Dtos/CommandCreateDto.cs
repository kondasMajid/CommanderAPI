
using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CommandCreateDto
    {
            // public int Id { get; set; }
              [Required][MaxLength(100)]
            public string Howto { get; set; }
            [Required]
            public string Line { get; set; }
            [Required]
            public string Platform { get; set; }
    }
}
