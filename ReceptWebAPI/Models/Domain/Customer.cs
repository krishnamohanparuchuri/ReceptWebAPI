namespace ReceptWebAPI.Models.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAvailable { get; set; }
    }
}
