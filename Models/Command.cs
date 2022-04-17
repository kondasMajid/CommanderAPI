
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
            [Key]
            public int Id { get; set; }
            [Required][MaxLength(100)]
            public string Howto { get; set; }
            public string Line { get; set; }
            public string Platform { get; set; }
    }
}


// representation of the main data in our application 