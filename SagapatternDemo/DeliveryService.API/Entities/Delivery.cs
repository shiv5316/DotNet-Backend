namespace DeliveryService.API.Entities
{
    public class Delivery
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Status { get; set; } // Delivered / Failed
    }
}
