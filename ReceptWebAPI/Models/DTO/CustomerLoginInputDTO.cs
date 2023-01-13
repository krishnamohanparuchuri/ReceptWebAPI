using System.ComponentModel.DataAnnotations;

namespace ReceptWebAPI.Models.DTO
{
    public class CustomerLoginInputDTO
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
