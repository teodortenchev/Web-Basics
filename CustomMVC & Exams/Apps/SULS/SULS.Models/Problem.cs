using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class Problem
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public int Points { get; set; }
    }
}
