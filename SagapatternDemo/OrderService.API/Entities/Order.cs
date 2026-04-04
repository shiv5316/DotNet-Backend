namespace OrderService.API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Items { get; set; }
        public string Status { get; set; }
    }
}
