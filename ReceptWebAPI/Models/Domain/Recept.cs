using System.ComponentModel.DataAnnotations;

namespace ReceptWebAPI.Models.Domain
{
    public class Recept
    {
        [Key]
        public int ReceptId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Ingredients { get; set; }
        public int Rating { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
