using System.ComponentModel.DataAnnotations;

namespace ReceptWebAPI.Models.DTO
{
    public class CustomerInsertInputDTO
    {
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
