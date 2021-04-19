using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models
{
    public class CommandAP 
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Platform { get; set; }
        [Required]
        public string CommandLine { get; set; }

    }
}