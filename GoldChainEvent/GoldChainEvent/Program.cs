using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService();
            var evaluationService = new GoldEvaluationService();
            var pricingService = new PricingService();
            var paymentService = new PaymentService();

            orderService.OrderPlaced += evaluationService.EvaluateGold;
            evaluationService.GoldEvaluated += pricingService.CalculatePrice;
            pricingService.PriceCalculated += paymentService.MakePayment;

            orderService.PlaceOrder(10); 
        }
    }
    public class OrderService
    {
        public event Action<int> OrderPlaced;

        public void PlaceOrder(int weight)
        {
            Console.WriteLine($"Order placed for {weight}g gold");

            OrderPlaced?.Invoke(weight);
        }
    }

    public class GoldEvaluationService
    {
        public event Action<int> GoldEvaluated;

        public void EvaluateGold(int weight)
        {
            Console.WriteLine($"Evaluating gold: {weight}g");

            // You can add purity logic here later

            GoldEvaluated?.Invoke(weight);
        }
    }

    public class PricingService
    {
        public event Action<double> PriceCalculated;

        private double pricePerGram = 6000;

        public void CalculatePrice(int weight)
        {
            double total = weight * pricePerGram;

            Console.WriteLine($"Price calculated: Rs{total}");

            PriceCalculated?.Invoke(total);
        }
    }

    public class PaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine($"Payment of Rs{amount} successful");
        }
    }
}
