using FacadePattern;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Child: Mom, I want ice cream!");
        IceCreamFacade mother = new IceCreamFacade();
        string iceCream = mother.GetIceCream();
        Console.WriteLine("Mother gives: " + iceCream);
        Console.ReadLine();
    }
}
