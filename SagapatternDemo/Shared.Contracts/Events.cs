namespace Shared.Contracts;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string Items { get; set; }
    public string CorrelationId { get; set; }
}

public class FoodPreparedEvent
{
    public int OrderId { get; set; }
    public string CorrelationId { get; set; }
}

public class FoodPreparationFailedEvent
{
    public int OrderId { get; set; }
    public string Reason { get; set; }
    public string CorrelationId { get; set; }
}

public class FoodDeliveredEvent
{
    public int OrderId { get; set; }
    public string CorrelationId { get; set; }
}

public class DeliveryFailedEvent
{
    public int OrderId { get; set; }
    public string Reason { get; set; }
    public string CorrelationId { get; set; }
}