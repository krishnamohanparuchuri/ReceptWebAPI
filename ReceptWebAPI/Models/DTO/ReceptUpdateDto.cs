using System.ComponentModel.DataAnnotations;

namespace ReceptWebAPI.Models.DTO
{
    public class ReceptUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Ingredients { get; set; }

    }
}
