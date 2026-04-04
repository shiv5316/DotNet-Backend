namespace KitchenService.API.Entities
{
    public class KitchenOrder
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Items { get; set; }

        public string Status { get; set; }
    }
}
